using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Controllers
{
    public class EmployeeController : Controller
    {
       [Authorize(Policy = "CityPolicy")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult City()
        {
            return View();
        }

    }
}
