using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Infrastructure.Services
{
    internal sealed class JwtProvider : IJWTProvider
    {
        public string CreateToken(AppUser appUser)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.FullName),
                new Claim(ClaimTypes.Email, appUser.Email ?? string.Empty),
                new Claim("Username", appUser.UserName ?? string.Empty),
            };

            DateTime expires = DateTime.Now.AddDays(1);

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("This is very long my security key"));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new(
                issuer: "Aydın BAŞKARA",
                audience: "eAppointment",
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: signingCredentials);

            JwtSecurityTokenHandler tokenHandler = new();

            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
