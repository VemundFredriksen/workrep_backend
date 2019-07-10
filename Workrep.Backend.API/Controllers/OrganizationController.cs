using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workrep.Backend.API.Models;
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

        /// <summary>
        /// Returns a list of organization specified by search criteria
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ClientOrganization[]>> GetAsync([FromQuery] OrganizationSearchCriteria searchCriteria)
        {
            return DBContext.Organization.Include(o => o.OrganizationBio)
                .Include(o => o.Workplace).ThenInclude(w => w.Review)
                .Select(o => new ClientOrganization()
                {
                    OrganizationNumber = o.OrganizationNumber,
                    Name = o.Name,
                    Homepage = o.Homepage,
                    City = o.City,
                    Country = o.Country,
                    Address = o.Address,
                    EmployeeCount = o.Employees,
                    SectorCode = o.SectorCode,
                    Type = o.Type,
                    ZIP = o.Zip,
                    Bio = o.OrganizationBio == null ? null : o.OrganizationBio.Bio,
                    WorkplaceCount = o.Workplace.Count,
                    ReviewCount = o.Workplace.Sum(w => w.Review.Count),
                    Rating = (float)o.Workplace.Sum(w => w.Review.Count) == 0 ? 0.0F : (float) o.Workplace.Average(w => w.Review.Average(r => r.Rating))

                }).Where(cw => (searchCriteria.Name == null) ? true : cw.Name.StartsWith(searchCriteria.Name) || cw.Name.Contains($" {searchCriteria.Name}"))
                .Where(cw => (searchCriteria.City == null) ? true : cw.City == searchCriteria.City)
                .Where(cw => (searchCriteria.Country == null) ? true : cw.Country == searchCriteria.Country)
                .Where(cw => cw.EmployeeCount >= searchCriteria.MinEmployees)
                .Where(cw => (searchCriteria.MaxEmployees == 0) ? true : cw.EmployeeCount <= searchCriteria.MaxEmployees)
                .Where(cw => (searchCriteria.MinWorkplaces == 0) ? true : cw.WorkplaceCount >= searchCriteria.MinWorkplaces )
                .Where(cw => (searchCriteria.MinReviewCount == 0) ? true : cw.ReviewCount >= searchCriteria.MinReviewCount)
                .ToArray();
        }

        /// <summary>
        /// Returns the specified organization
        /// </summary>
        /// <param name="organizationNumber">Organization number</param>
        /// <returns>Organization</returns>
        [HttpGet("{organizationNumber}")]
        public async Task<ActionResult<ClientOrganization>> GetAsync(long organizationNumber)
        {
            var organization = DBContext.Organization.Include(o => o.OrganizationBio)
                .Include(o => o.Workplace)
                .ThenInclude(w => w.Review)
                .Select(o => new ClientOrganization()
                {
                    OrganizationNumber = o.OrganizationNumber,
                    Name = o.Name,
                    Homepage = o.Homepage,
                    City = o.City,
                    Country = o.Country,
                    Address = o.Address,
                    EmployeeCount = o.Employees,
                    SectorCode = o.SectorCode,
                    Type = o.Type,
                    ZIP = o.Zip,
                    Bio = o.OrganizationBio == null ? null : o.OrganizationBio.Bio,
                    WorkplaceCount = o.Workplace.Count,
                    ReviewCount = o.Workplace.Sum(w => w.Review.Count),
                    Rating = (float)o.Workplace.Sum(w => w.Review.Count) == 0 ? 0.0F : (float)o.Workplace.Average(w => w.Review.Average(r => r.Rating))
                }).SingleOrDefault(org => org.OrganizationNumber == organizationNumber);

            if (organization == null)
                return NotFound($"Organization {organizationNumber} not found!");

            return organization;
        }

        /// <summary>
        /// Returns list of all workplaces associated with specified organization
        /// </summary>
        /// <param name="organizationNumber">Organization number</param>
        /// <returns>List of Workplaces</returns>
        [HttpGet("{organizationNumber}/workplaces")]
        public async Task<ActionResult<ClientWorkplace[]>> GetWorkplacesAsync(long organizationNumber)
        {
            return DBContext.Workplace.Include(w => w.Review)
                .Include(w => w.WorkplaceBio)
                .Include(w => w.SuperOrganizationNumberNavigation)
                .Where(w => w.SuperOrganizationNumber == organizationNumber)
                .Select(w => new ClientWorkplace()
                {
                    EmployeeCount = w.Employees,
                    OrganizationNumber = w.OrganizationNumber,
                    Address = w.Address,
                    Bio = w.WorkplaceBio == null ? null : w.WorkplaceBio.Bio,
                    City = w.City,
                    Country = w.Country,
                    Homepage = w.Homepage,
                    Name = w.Name,
                    Rating = w.Review.Count == 0 ? 0.0F : (float) w.Review.Average(r => (decimal) r.Rating),
                    ReviewCount = w.Review.Count,
                    SuperName = w.SuperOrganizationNumberNavigation.Name,
                    SuperOrganizationNumber = w.SuperOrganizationNumber,
                    ZIP = w.Zip
                }).ToArray();
        }
    }
}