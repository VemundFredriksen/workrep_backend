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
    public class WorkplaceController : Controller
    {

        public WorkrepContext DBContext { get; set; }

        public WorkplaceController(WorkrepContext dbContext)
        {
            this.DBContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<ClientWorkplace[]>> GetAllAsync()
        {
            return DBContext.Workplace.Include(w => w.Review)
                .Include(w => w.SuperOrganizationNumberNavigation)
                .Include(w => w.WorkplaceBio)
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
                    Rating = w.Review.Count == 0 ? 0.0F : (float)w.Review.Average(r => (decimal)r.Rating),
                    ReviewCount = w.Review.Count,
                    SuperName = w.SuperOrganizationNumberNavigation.Name,
                    SuperOrganizationNumber = w.SuperOrganizationNumber,
                    ZIP = w.Zip
                }).ToArray();
        }

        [HttpGet("{organizationNumber}")]
        public async Task<ActionResult<ClientWorkplace>> GetAsync(long organizationNumber)
        {
            return DBContext.Workplace.Include(w => w.Review)
                .Include(w => w.SuperOrganizationNumberNavigation)
                .Include(w => w.WorkplaceBio)
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
                    Rating = w.Review.Count == 0 ? 0.0F : (float)w.Review.Average(r => (decimal)r.Rating),
                    ReviewCount = w.Review.Count,
                    SuperName = w.SuperOrganizationNumberNavigation.Name,
                    SuperOrganizationNumber = w.SuperOrganizationNumber,
                    ZIP = w.Zip
                }).SingleOrDefault(cw => cw.OrganizationNumber == organizationNumber);
        }

    }
}