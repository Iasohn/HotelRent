using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentRoom.Models;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentRoom.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        public string CreateToken(User user)
        {
            var _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256);
            var _audience = _configuration["JWT:Audience"];
            var _issuer = _configuration["JWT:Issuer"];

            var _claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("id", user.Id)
            };

            var Token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: _claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
                );

            Console.WriteLine(Token);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
