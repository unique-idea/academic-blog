﻿using Academic_Blog.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Academic_Blog.Utils
{
    public class JwtUtil
    {
        private JwtUtil()
        {

        }
        public static string GenerateJwtToken(Account account, Tuple<string, Guid> guidClaim)
        {
            IConfiguration config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
            .Build();
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
            string issuer = config["Jwt:Issuer"];
            List<Claim> claims = new List<Claim>()
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,account.UserName),
                new Claim(ClaimTypes.Role,account.Role.Name)
            };
            if(guidClaim != null) claims.Add(new Claim(guidClaim.Item1,guidClaim.Item2.ToString()));
            var expires = DateTime.Now.AddHours(3);
            var token = new JwtSecurityToken(issuer, null,claims, notBefore : DateTime.Now, expires, credentials);
            return jwtHandler.WriteToken(token);
        }   
    }
}
