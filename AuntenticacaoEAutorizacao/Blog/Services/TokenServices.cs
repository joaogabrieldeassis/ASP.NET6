﻿using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Services;
    public class TokenServices
    {
        //Dado um usuario, vamos gerar um token apartir desse método
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "João Gabriel"),
                new (ClaimTypes.Role, "adimin"),
                new ("Fruta", "Banana")
            }),
                 Expires = DateTime.UtcNow.AddHours(8),
                

                 SigningCredentials = new SigningCredentials
                 (
                  new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature
                 )
             };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
               
        
    }

 