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
    public class UserController : Controller, WorkrepAPIController
    {

        public WorkrepContext DBContext { get; private set; }
        private AuthenticationService AuthService { get; set; } = null;

        public UserController(WorkrepContext dbContext, AuthenticationService authService)
        {
            this.DBContext = dbContext;
            this.AuthService = authService;
        }

        /// <summary>
        /// Returns userinfo for authenticated user
        /// </summary>
        /// <returns>Userinfo</returns>
        [HttpGet]
        public async Task<ActionResult<ClientUser>> GetAsync()
        {
            var user = this.GetUser();
            if (user == null)
                return Unauthorized();

            return new ClientUser(user);
        }

        /// <summary>
        /// Returns all reviews written by authenticated user
        /// </summary>
        /// <returns>List of reviews</returns>
        [HttpGet("Reviews")]
        public async Task<ActionResult<ClientReview[]>> GetReviewsAsync()
        {
            var user = this.GetUser();
            if (user == null)
                return Unauthorized();

            var query = (from reviews in DBContext.Review
                         join workplaces in DBContext.Workplace on reviews.WorkplaceOrganizationNumber equals workplaces.OrganizationNumber
                         where reviews.UserId == user.UserId
                         select new { Review = reviews, Workplace = workplaces });

            if (query == null)
                return new ClientReview[0];

            var userReviews = new List<ClientReview>();
            foreach(var row in query)
            {
                userReviews.Add(new ClientReview(row.Review, row.Workplace));
            }

            return userReviews.ToArray();

        }
    }
}