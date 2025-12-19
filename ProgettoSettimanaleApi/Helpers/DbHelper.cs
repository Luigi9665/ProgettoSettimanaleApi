using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanaleApi.Exceptions;
using ProgettoSettimanaleApi.Model.Entity;

namespace ProgettoSettimanaleApi.Helpers
{
    public static class DbHelper
    {

        //metodo per l'inizializzazione del database - con tutte le chiamate ai metodi privati
        public static async Task InitializeDatabaseAsync<T>(WebApplication app) where T : DbContext
        {
            try
            {
                IServiceProvider services = app.Services;

                await RunMigrationAsync<T>(services);
                await SeedRoles(services);
                await SeedAdmin(services);
            }
            catch
            {
                throw;
            }
        }


        //metodo per eseguire le migrazioni
        private static async Task RunMigrationAsync<T>(IServiceProvider services) where T : DbContext
        {
            try
            {
                using var scope = services.CreateAsyncScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<T>();

                await dbContext.Database.MigrateAsync();
            }
            catch
            {
                throw;
            }
        }

        //metodo per il seeding dei ruoli
        private static async Task SeedRoles(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateAsyncScope();

                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                bool superAdminRoleExists = await roleManager.RoleExistsAsync(StringConstants.SuperAdminRole);

                IdentityRole? superAdminRole = null;
                IdentityRole? userRole = null;

                if (!superAdminRoleExists)
                {
                    superAdminRole = new IdentityRole()
                    {
                        Name = StringConstants.SuperAdminRole,
                    };

                    IdentityResult superAdminRoleCreated = await roleManager.CreateAsync(superAdminRole);

                    if (!superAdminRoleCreated.Succeeded)
                    {
                        throw new DbInitilizationException("Errore durante la creazione del ruolo SuperAdmin");
                    }
                }

                bool userRoleExists = await roleManager.RoleExistsAsync(StringConstants.UserRole);

                if (!userRoleExists)
                {
                    userRole = new IdentityRole()
                    {
                        Name = StringConstants.UserRole,
                    };

                    IdentityResult userRoleCreated = await roleManager.CreateAsync(userRole);

                    if (!userRoleCreated.Succeeded)
                    {
                        if (superAdminRole != null)
                        {
                            await roleManager.DeleteAsync(superAdminRole);
                        }
                        throw new DbInitilizationException("Errore durante la creazione del ruolo User");
                    }
                }

            }
            catch
            {
                throw;
            }
        }


        //metodo per il seeding dell'admin
        private static async Task SeedAdmin(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateAsyncScope();

                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                ApplicationUser? existingSuperAdmin = await userManager.FindByEmailAsync(StringConstants.SaEmail);

                if (existingSuperAdmin == null)
                {
                    ApplicationUser superAdmin = new ApplicationUser()
                    {
                        DateOfBirth = new DateOnly(1996, 12, 30),
                        Name = "Luigi",
                        LastName = "Ventriglia",
                        Email = StringConstants.SaEmail,
                        UserName = "LuigiEpicode",
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    IdentityResult userCreated = await userManager.CreateAsync(superAdmin, "Epicode2025");

                    if (!userCreated.Succeeded)
                    {
                        throw new DbInitilizationException("Errore durante la creazione dell'utente SuperAdmin");
                    }

                    IdentityResult roleAssigned = await userManager.AddToRoleAsync(superAdmin, StringConstants.SuperAdminRole);

                    if (!roleAssigned.Succeeded)
                    {
                        await userManager.DeleteAsync(superAdmin);
                        throw new DbInitilizationException("Errore durante l'assegnamento del ruolo all'utente");
                    }
                }

            }
            catch
            {
                throw;
            }
        }


    }
}
