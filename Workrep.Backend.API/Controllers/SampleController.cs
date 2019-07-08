using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    
    public class SampleController : Controller
    {

        private WorkrepContext DBContext { get; set; }

        public SampleController(WorkrepContext dbContext)
        {
            DBContext = dbContext;
        }

        [HttpGet]
        public ActionResult<User> Index()
        {
            return DBContext.User.First();
        }
    }
}