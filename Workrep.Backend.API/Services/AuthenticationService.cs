﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Services
{
    public class AuthenticationService
    {

        public string Secret { get; set; }
        public int ExpireTime { get; set; }

        private JwtSecurityTokenHandler TokenHandler { get; set; }

        public AuthenticationService()
        {
            this.TokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateToken(User user)
        {
            var symmetricKey = Convert.FromBase64String(Secret);

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user_id", user.UserId.ToString())
                }),

                Expires = now.AddMinutes(Convert.ToInt32(30)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = TokenHandler.CreateToken(tokenDescriptor);
            var token = TokenHandler.WriteToken(stoken);

            return token;
        }

        public bool ValidateToken(string token)
        {
            SecurityToken identiy;

            var simplePrinciple = TokenHandler.ValidateToken(token, null, out identiy);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst("user_id");
            var username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

    }
}