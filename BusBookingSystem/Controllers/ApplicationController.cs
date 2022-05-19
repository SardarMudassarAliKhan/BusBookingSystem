using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Helper.AzureBlob;
using CVBank.Dto.API;
using CVBank.InfraStructure.Service.JobApplication;
using CVBank.InfraStructure.Service.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CVBank.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IJobApplicationService _jobApplicationService;
        public ApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> JobApplication([FromForm]JobApplicationDto request)
        {
            var file = Request.Form.Files[0];
            string dbPath = string.Empty;
            if (file.Length > 0)
            {
                request.CvUrl = await UploadAzureBlobStorage.UploadFileAzure(file);
            }
            else
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                // the code below should probably be refactored into a GetModelErrors
                // method on your BaseApiController or something like that

                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return Ok(new { Status = 400,Data = errors, Message = "One or more validation errors occurred." });
            }
            var result = await _jobApplicationService.SaveJobApplication(request);
            return Ok(new { Status = 200, Data = result, Message = "Success" });
        }
    }
}
