using CVBank.Dto.API;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Service.JobApplication
{
    public interface IJobApplicationService
    {
        Task<bool> SaveJobApplication(JobApplicationRequestDto request);
        Task<bool> SaveJobApplication(JobApplicationDto request);
        Task<bool> UpdateJobApplication(JobApplicationUpdateRequestDto request);
        Task<JobApplicationRequestDto> GetJobApplication(long applicationId);
        Task<List<JobApplicationResponseDto>> JobApplications();
    }
}
