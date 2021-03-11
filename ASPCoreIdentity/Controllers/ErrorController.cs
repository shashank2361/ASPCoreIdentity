using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Controllers
{


    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;


        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;

        }

        // If there is 404 status code, the route path will become Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            
            var statusCodeResult =
                    HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            
            
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage =
                            "Sorry, the resource you requested could not be found";
                    ViewBag.Path = statusCodeResult?.OriginalPath;
                    ViewBag.QS = statusCodeResult?.OriginalQueryString;
                    logger.LogWarning($"404 error occured. Path = " +
                 $"{statusCodeResult.OriginalPath} and QueryString = " +
                 $"{statusCodeResult.OriginalQueryString}");

                    break;
            }

            return View("NotFound");
        }


        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

             logger.LogError($"The path {exceptionHandlerPathFeature.Path} " +
                $"threw an exception {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }


        //[AllowAnonymous]
        //[Route("Error")]
        //public IActionResult Error()
        //{
        //    // Retrieve the exception Details
        //    var exceptionHandlerPathFeature =
        //            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        //    ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
        //    ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
        //    ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

        //    return View("Error");
        //}
    }
}