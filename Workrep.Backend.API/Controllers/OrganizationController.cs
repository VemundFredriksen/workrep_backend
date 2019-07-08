using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {

        private WorkrepContext DBContext { get; set; }

        public OrganizationController(WorkrepContext dbContext)
        {
            DBContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Organization[]>> GetAllAsync()
        {
            return DBContext.Organization.ToArray();
        }

        [HttpGet("{organizationNumber}")]
        public async Task<ActionResult<Organization>> GetAsync(long organizationNumber)
        {
            var organization = DBContext.Organization.SingleOrDefault(org => org.OrganizationNumber == organizationNumber);
            if (organization == null)
                return NotFound($"Organization {organizationNumber} not found!");

            return organization;
        }

        [HttpGet("{organizationNumber}/workplaces")]
        public async Task<ActionResult<Workplace[]>> GetWorkplacesAsync(long organizationNumber)
        {
            return DBContext.Workplace.Where(work => work.SuperOrganizationNumber == organizationNumber).ToArray();
            
        }

    }
}