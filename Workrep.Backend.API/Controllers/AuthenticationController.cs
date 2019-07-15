using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workrep.Backend.API.Models;
using Workrep.Backend.DatabaseIntegration.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Workrep.Backend.API.Services;

namespace Workrep.Backend.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
       
        private WorkrepContext DBContext { get; set; }
        private AuthenticationService AuthService { get; set; }

        public AuthenticationController(WorkrepContext dbContext, AuthenticationService authService)
        {
            this.DBContext = dbContext;
            this.AuthService = authService;

        }

        [HttpGet]
        public async Task<ActionResult<string>> GetToken([FromQuery] UserLoginCredentials loginCredentials)
        {
            var user = DBContext.User.SingleOrDefault(u => u.Email == loginCredentials.Email && u.Password == loginCredentials.Password);
            if (user == null)
                return NotFound("User not found");

            return this.AuthService.GenerateToken(user);
        }

    }
}