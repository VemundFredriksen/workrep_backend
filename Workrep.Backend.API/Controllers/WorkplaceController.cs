﻿using System;
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

        /// <summary>
        /// TODO : Should be removed
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns specified workplace
        /// </summary>
        /// <param name="organizationNumber">Organization number</param>
        /// <returns>Workplace</returns>
        [HttpGet("{organizationNumber}")]
        public async Task<ActionResult<ClientWorkplace>> GetAsync(long organizationNumber)
        {
            var workplace = DBContext.Workplace.Include(w => w.Review)
                .Include(w => w.SuperOrganizationNumberNavigation)
                .Include(w => w.WorkplaceBio)
                .SingleOrDefault(w => w.OrganizationNumber == organizationNumber);

            if (workplace == null)
                return NotFound($"Workplace {organizationNumber} not found");

            return new ClientWorkplace()
            {
                EmployeeCount = workplace.Employees,
                OrganizationNumber = workplace.OrganizationNumber,
                Address = workplace.Address,
                Bio = workplace.WorkplaceBio == null ? null : workplace.WorkplaceBio.Bio,
                City = workplace.City,
                Country = workplace.Country,
                Homepage = workplace.Homepage,
                Name = workplace.Name,
                Rating = workplace.Review.Count == 0 ? 0.0F : (float)workplace.Review.Average(r => (decimal)r.Rating),
                ReviewCount = workplace.Review.Count,
                SuperName = workplace.SuperOrganizationNumberNavigation.Name,
                SuperOrganizationNumber = workplace.SuperOrganizationNumber,
                ZIP = workplace.Zip
            };
        }

        /// <summary>
        /// Returns list of reviews associated with specified workplace
        /// </summary>
        /// <param name="organizationNumber">Organization number</param>
        /// <returns>List of Reviews</returns>
        [HttpGet("{organizationNumber}/reviews")]
        public async Task<ActionResult<Review[]>> GetReviewsAsync(long organizationNumber)
        {
            return DBContext.Review.Where(r => r.WorkplaceOrganizationNumber == organizationNumber).ToArray();
        }

    }
}