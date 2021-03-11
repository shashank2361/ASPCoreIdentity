using ASPCoreIdentity.Models;
using ASPCoreIdentity.Security;
using ASPCoreIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        public IEmployeeRepository _employeeRepository { get; }
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;
        private readonly IDataProtectionProvider dataProtectionProvider;
        //private readonly DataProtectionPurposeStrings dataProtectionPurposeStrings;
        private readonly IDataProtector protector;

        public HomeController(IEmployeeRepository employeeRepository , IHostingEnvironment hostingEnvironment ,
                        ILogger<HomeController> logger, IDataProtectionProvider dataProtectionProvider ,
                        DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.dataProtectionProvider = dataProtectionProvider;
            //this.dataProtectionPurposeStrings = dataProtectionPurposeStrings;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }


        [Route("")]
        [Route("Index")]
        [Route("~/")]
        public IActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployees().Select(e =>
           {
               e.EncryptedId = protector.Protect(e.Id.ToString());
               return e;
           });


            return View(model);
        }

        [HttpGet]
        [Route("Details/{id}")]

        //public ViewResult Details(int id)
        public ViewResult Details(string id)
        {
            //this changes is after encrpting the id in view , this is to decrypt the id 
            int employeeId = Convert.ToInt32( protector.Unprotect(id));
             HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(employeeId),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        public RedirectToActionResult Create(Employee employee)
        {
            Employee newEmployee = _employeeRepository.Add(employee);
            return RedirectToAction("details", new { id = newEmployee.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Edit/{id}")]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [Authorize]

        [Route("Update")]
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                // Update the employee object with the data in the model object
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department ?? 0;

           

            if (model.Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (model.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                        "images", model.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object which will be
                // eventually saved in the database
                employee.PhotoPath = ProcessUploadedFile(model);
            }

            // Call update method on the repository service passing it the
            // employee object to update the data in the database table
            Employee updatedEmployee = _employeeRepository.Update(employee);

            return RedirectToAction("index");
        }

    return View(model);
    }



        [Authorize]
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department ?? 0,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    

    }
}
