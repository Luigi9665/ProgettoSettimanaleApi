using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProgettoSettimanaleApi.Model.DTOs.Requests;
using ProgettoSettimanaleApi.Model.DTOs.Responses;
using ProgettoSettimanaleApi.Model.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProgettoSettimanaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Save(ProgettoSettimanaleApi.Model.DTOs.Requests.RegisterRequest registerRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // effettuo la registrazione
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerRequest.UserName,
                        Email = registerRequest.Email,
                        Name = registerRequest.FirsName,
                        LastName = registerRequest.LastName,
                        CreatedAt = DateTime.Now,
                        Id = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);


                    if (result.Succeeded)
                    {
                        var role = await _roleManager.RoleExistsAsync("User");

                        if (!role)
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await _userManager.AddToRoleAsync(user, "User");
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Model.DTOs.Requests.LoginRequest loginRequest)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(loginRequest.Email);

                    if (user is not null)
                    {

                        //Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                        //    user,
                        //    loginRequest.Password,
                        //    isPersistent: false,
                        //    lockoutOnFailure: false
                        //    );

                        //if (!result.Succeeded)
                        //{
                        //    return BadRequest();
                        //}

                        bool passwordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

                        if (!passwordValid)
                        {
                            return Unauthorized();
                        }

                        List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();

                        List<Claim> userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                        };

                        foreach (string roleName in roles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }

                        var key = System.Text.Encoding.UTF8.GetBytes("ab42a05ce51a0b5fd272b4e666a7f31fe17430fcc74c20ea00f622aabb82a9f8");
                        SigningCredentials cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                        var tokenExpiration = DateTime.Now.AddMinutes(30);

                        JwtSecurityToken jwt = new JwtSecurityToken(
                            issuer: "https://",
                            audience: "https://",
                            claims: userClaims,
                            expires: tokenExpiration,
                            signingCredentials: cred
                            );

                        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

                        return Ok(new LoginResponse
                        {
                            Token = token,
                            Expiration = tokenExpiration
                        });

                    }
                    else
                    {
                        return BadRequest();
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }


    }
}
