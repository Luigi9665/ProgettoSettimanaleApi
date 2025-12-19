using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanaleApi.Model.Entity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }

        public DbSet<Artista> Artisti { get; set; }

        public DbSet<Evento> Eventi { get; set; }

        public DbSet<Biglietto> Biglietti { get; set; }

    }
}
