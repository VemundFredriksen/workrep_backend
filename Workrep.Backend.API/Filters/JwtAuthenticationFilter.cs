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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
                if(context.Filters.Any(filter => filter is AllowAnonymousFilter))
                {
                    return;
                }
                

                var authService = (AuthenticationService)context.HttpContext.RequestServices.GetService(typeof(AuthenticationService));

                string requestToken = context.HttpContext.Request.Headers["Authorization"];
                if (requestToken == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                //TODO This seems weird
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
