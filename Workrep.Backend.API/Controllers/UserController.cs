﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workrep.Backend.API.Models;
using Workrep.Backend.API.Models.HttpModels;
using Workrep.Backend.API.Services;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    [JwtAuthentication]
    [Route("api/[controller]")]
    public class UserController : Controller, WorkrepAPIController
    {

        public WorkrepContext DBContext { get; private set; }
        public EmailService EmailService { get; private set; }
        private AuthenticationService AuthService { get; set; } = null;

        public UserController(WorkrepContext dbContext, AuthenticationService authService, EmailService emailSaltService)
        {
            this.DBContext = dbContext;
            this.EmailService = emailSaltService;
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

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="body">Parameters for new user account</param>
        /// <returns>User</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ClientUser>> RegisterAsync([FromBody] UserRegistrationBody body)
        {
            var validity = this.ValidateModel();
            if (!validity.IsValid)
                return validity.ActionResult;

            if (DBContext.User.Any(u => u.Email == body.Email))
                return this.EmailIsTaken(body.Email);

            var user = new User()
            {
                Email = body.Email,
                Password = body.Password,
                Name = body.Name,
                Birthdate = body.Birthdate,
                //TODO Actually use valid data for gender
                Gender = false,
                Confirmed = false,
                RegisterDate = DateTime.UtcNow
            };

            DBContext.User.Add(user);

            //Create new email confirmation ticket
            var ticket = this.GenerateEmailConfirmationTicket(user);
            var ticketEntity = new EmailConfirmation()
            {
                GenKey = ticket,
                UserId = user.UserId
            };

            DBContext.EmailConfirmation.Add(ticketEntity);

            //TODO asycn send mail

            DBContext.SaveChanges();

            return new ClientUser(user);
        }

        /// <summary>
        /// Confirms the users email address
        /// </summary>
        /// <param name="ticket">The generated ticket that proves the users email</param>
        /// <returns>Success Page</returns>
        [AllowAnonymous]
        [HttpGet("email/confirmation/{ticket}")]
        public async Task<ActionResult> ConfirmEmailAsync(string ticket)
        {
            var confirmation = DBContext.EmailConfirmation.FirstOrDefault(e => e.GenKey == ticket);

            //TODO Redirect to homepage for requesting new key
            if (confirmation == null)
                return BadRequest();

            DBContext.User.First(u => u.UserId == confirmation.UserId).Confirmed = true;
            DBContext.EmailConfirmation.Remove(confirmation);
            DBContext.SaveChanges();

            //TODO return to a nice view
            return Ok();

        }

        [HttpPut("email/confirmation")]
        public async Task<ActionResult> ResendEmailConfirmationAsync()
        {
            var user = this.GetUser();
            if (user == null)
                return Unauthorized();

            if ((bool)user.Confirmed)
                return BadRequest("Email is already confirmed!");

            var ticket = this.GenerateEmailConfirmationTicket(user);

            var existingTicket = DBContext.EmailConfirmation.FirstOrDefault(c => c.UserId == user.UserId);
            if (existingTicket != null)
                DBContext.EmailConfirmation.Remove(existingTicket);

            var ticketEntity = new EmailConfirmation()
            {
                GenKey = ticket,
                UserId = user.UserId
            };

            DBContext.EmailConfirmation.Add(ticketEntity);
            EmailService.SendEmailConfirmationMail(user, ticket);
            DBContext.SaveChanges();
            return Ok();

        }

    }
}