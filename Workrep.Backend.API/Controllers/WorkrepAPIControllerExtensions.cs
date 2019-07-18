using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Controllers
{
    public static class WorkrepAPIControllerExtensions
    {

        public static ObjectResult OrganizationNotFound(this WorkrepAPIController controller, long organizationNumber)
        {
            return new NotFoundObjectResult($"Organization {organizationNumber} not found.");
        }

    }
}
