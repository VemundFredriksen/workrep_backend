using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workrep.Backend.API.Models
{
    public class ModelValidity
    {
        public bool IsValid { get; set; }
        public ActionResult ActionResult { get; set; }
    }
}
