using ASPCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Security
{
    public class CityPolicyRequirment : IAuthorizationRequirement
    {
         
    }

    public class CityPolicyRequirmentHandler : AuthorizationHandler<CityPolicyRequirment>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CityPolicyRequirmentHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }



        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            CityPolicyRequirment requirement)
        {
            var authFilterContext = context.Resource as AuthorizationFilterContext;
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }


            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];


            var loggedInAdmin =
                   context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var appUser = userManager?.FindByIdAsync(loggedInAdmin).Result;
 
            var user = context.User;

            var city = appUser.City;
            if (city == "London")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
         
            return Task.CompletedTask;


        }

    }
}
