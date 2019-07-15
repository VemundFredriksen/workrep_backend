using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workrep.Backend.API.Models;
using Workrep.Backend.API.Services;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    [JwtAuthentication]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private WorkrepContext DBContext { get; set; }
        private AuthenticationService AuthService { get; set; } = null;

        public UserController(WorkrepContext dbContext, AuthenticationService authService)
        {
            this.DBContext = dbContext;
            this.AuthService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var x = (int) HttpContext.Items["userId"];
            return DBContext.User.SingleOrDefault(u => u.UserId == x);
        }
    }
}