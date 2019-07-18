using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Controllers
{
    public interface WorkrepAPIController
    {

        HttpContext HttpContext { get; }
        WorkrepContext DBContext { get; }

    }
}
