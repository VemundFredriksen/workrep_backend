using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Workrep.Backend.API.Services;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Workrep.Backend.API.Models
{

    public class JwtAuthentication : TypeFilterAttribute
    {
        public JwtAuthentication() : base(typeof(JwtAuthenticationImpl))
        {

        }

        private class JwtAuthenticationImpl : IActionFilter
        {

            public void OnActionExecuted(ActionExecutedContext context)
            {
                context.Result = new UnauthorizedResult();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {

            }
        }
    }

    
}
