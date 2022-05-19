using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CVBank.AzureDataLakeService;
using CVBank.DataLakeExtensions;
using CVBank.Dto.ConfigurationModel;
using CVBank.Dto.Dtos;
using CVBank.Filters;
using CVBank.InfraStructure.Service.File;
using CVBank.InfraStructure.Service.User;
using CVBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBank.InfraStructure.Service.JobApplication;
using Common.Helper.AzureBlob;

namespace CVBank.Controllers
{
    [CustomAuthorize]
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJobApplicationService _jobApplicationService;
        public DashboardController(IUserService userService, IJobApplicationService jobApplicationService)
        {
            _userService = userService;
            _jobApplicationService = jobApplicationService;
        }
        public  IActionResult Index()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }

        public IActionResult AccountManagement()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }

        [HttpPost]
        public IActionResult BusesAnalysis(string query)
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
 
        public async Task<IActionResult> EditAccount()
        {
            ViewData["Title"] = SetTitle();
            var user = await _userService.GetUserByName(this.User.Identity.Name);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserDto request)
        {
            if (ModelState.IsValid) {
                var result = await _userService.UpdateUser(request);
                if (result.Succeeded) return RedirectToAction("Index","Dashboard");
            }
            return View(request);
        }
        public IActionResult UpdatePassword()
        {
            ViewData["Title"] = SetTitle();
            var model = new UpdatePasswordDto();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto request)
        {
            if (ModelState.IsValid)
            {
                request.Email = this.User.Identity.Name;
                var result = await _userService.UpdatePassword(request);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(request);
        }
        [NonAction]
        public string SetTitle()
        {
            return (this.User.Identity.IsAuthenticated) ? this.User.Identity.Name : "Bus Booking App";
        }


        public async Task<IActionResult> Users()
        {
            ViewData["Title"] = SetTitle();
            var model = await _userService.GetUsers();
            return View(model);
        }
        public IActionResult DriverAnalysis()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
        public IActionResult BusesAnalysis()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
        public IActionResult FinancialProgress()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
        public IActionResult CustomerAnalysis()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
        public IActionResult HrDetails()
        {
            ViewData["Title"] = SetTitle();
            return View();
        }
        public IActionResult AddJobApplication()
        {
            ViewData["Title"] = "Create Job Application";
            return View(new JobApplicationRequestDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobApplication(JobApplicationRequestDto request)
        {
            if (request.FormFile == null || request.FormFile.Length == 0)
            {
                return Content("file not selected");
            }
                

                request.CvUrl = await UploadAzureBlobStorage.UploadFileAzure(request.FormFile);
                if(string.IsNullOrEmpty(request.CvUrl)) return Content("Unable to Upload CV.");
                if(!ModelState.IsValid) return View(request);

                var result = await _jobApplicationService.SaveJobApplication(request);
                return RedirectToAction("JobApplications", "Dashboard");
        }
        public async Task<IActionResult> UpdateJobApplication([FromQuery] long applicationId = 0)
        {
            ViewData["Title"] = "Update Job Application";
            var model = await _jobApplicationService.GetJobApplication(applicationId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateJobApplication(JobApplicationUpdateRequestDto request)
        {
            if (!ModelState.IsValid) return View(request);
            var result = await _jobApplicationService.UpdateJobApplication(request);
            return RedirectToAction("JobApplications", "Dashboard");
        }
    } 
    


}
