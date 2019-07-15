using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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

        public bool ValidateToken(string token, out int userId)
        {
            userId = -1;

            var simplePrinciple = this.GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var userIdClaim = identity.FindFirst("user_id");
            if (userIdClaim == null)
                return false;

            userId = Int32.Parse(userIdClaim.Value);

            if (userId == -1)
                return false;

            // More validate to check whether username exists in system

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            int userId;

            if (ValidateToken(token, out userId))
            {
                // based on username to get more information from database 
                // in order to build local identity
                var claims = new List<Claim>
        {
            new Claim("userid", userId.ToString())
            // Add more claims if needed: Roles, ...
        };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(this.Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
