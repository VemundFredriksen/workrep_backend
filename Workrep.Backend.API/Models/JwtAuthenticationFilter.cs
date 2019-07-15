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
                
                
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var authService = (AuthenticationService)context.HttpContext.RequestServices.GetService(typeof(AuthenticationService));

                string requestToken = context.HttpContext.Request.Headers["Authorization"];
                if (requestToken == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                requestToken = requestToken.Split(" ")[1];

                int userId;
                var authStatus = authService.ValidateToken(requestToken, out userId);
                if (!authStatus)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                context.HttpContext.Items["userId"] = userId;


                return;
            }
        }
    }

    
}
