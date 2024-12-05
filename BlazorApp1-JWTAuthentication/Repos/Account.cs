using BlazorApp1.JWTAuthentication.Data;
using BlazorApp1.JWTAuthentication.DTOs;
using BlazorApp1.JWTAuthentication.Models;
using BlazorApp1.JWTAuthentication.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BlazorApp1.JWTAuthentication.Responses.CustomResponses;

namespace BlazorApp1.JWTAuthentication.Repos
{
    public class Account : IAccount
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }

        public async Task<LoginResponse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);

            if (findUser == null) return new LoginResponse(false, "User does not exist");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser.Password))
            {
                return new LoginResponse(false, "Email/Password not valid");
            }
            var token = GenerateToken(findUser);

            return new LoginResponse(true, "Login Successfully", token);
        }

        public async Task<CustomResponses.RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);

            if (findUser != null) return new CustomResponses.RegistrationResponse(false, "User already exist");

            appDbContext.Users.Add(
                    new ApplicationUser()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                    }
                );

            await appDbContext.SaveChangesAsync();

            return new RegistrationResponse(true, "Success");
        }

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"]!,
                audience: config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> GetUser(string email) => await appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
    }
}
