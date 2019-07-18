using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Controllers
{
    public static class WorkrepAPIControllerExtensions
    {

        public static NotFoundObjectResult OrganizationNotFound(this WorkrepAPIController controller, long organizationNumber)
        {
            return new NotFoundObjectResult($"Organization {organizationNumber} not found.");
        }

        public static NotFoundObjectResult WorkplaceNotFound(this WorkrepAPIController controller, long organizationNumber)
        {
            return new NotFoundObjectResult($"Workplace {organizationNumber} not found.");
        }

    }
}
