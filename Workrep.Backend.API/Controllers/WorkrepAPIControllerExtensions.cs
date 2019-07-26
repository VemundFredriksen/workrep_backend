using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.API.Models;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    public static class WorkrepAPIControllerExtensions
    {

        public static User GetUser(this WorkrepAPIController controller)
        {
            int userId = (int) controller.HttpContext.Items["userId"];
            return controller.DBContext.User.FirstOrDefault(user => user.UserId == userId); 
        }

        public static ModelValidity ValidateModel(this Controller controller)
        {
            if (controller.ModelState.IsValid)
            {
                return new ModelValidity()
                {
                    IsValid = true,
                    ActionResult = new OkObjectResult("")
                };
            }

            return new ModelValidity()
            {
                IsValid = false,
                ActionResult = new BadRequestObjectResult(new { ErrorMessages = controller.ModelState.Values.SelectMany(e => e.Errors.Select(err => err.ErrorMessage)) })
            };


        }

        public static NotFoundObjectResult OrganizationNotFound(this WorkrepAPIController controller, long organizationNumber)
        {
            return new NotFoundObjectResult($"Organization {organizationNumber} not found.");
        }

        public static NotFoundObjectResult WorkplaceNotFound(this WorkrepAPIController controller, long organizationNumber)
        {
            return new NotFoundObjectResult($"Workplace {organizationNumber} not found.");
        }

        public static BadRequestObjectResult EmailIsTaken(this UserController controller, string email)
        {
            return new BadRequestObjectResult($"There is already an user with email {email} registered!");
        }

    }
}
