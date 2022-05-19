using CVBank.Dto.Dtos;
using CVBank.InfraStructure.Repository.Application;
using CVBank.Domain.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper;
using CVBank.Dto.API;

namespace CVBank.InfraStructure.Service.JobApplication
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        public JobApplicationService(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<JobApplicationRequestDto> GetJobApplication(long applicationId)
        {
            var application = await _jobApplicationRepository.JobApplication(applicationId);
            if (application is null) return null;
            var jobApplication = new JobApplicationRequestDto()
            {
                Id = application.Id,
                Name = application.Name,
                Email = application.Email,
                Phone = application.Phone,
                CvUrl = application.CvUrl,
                YearsOfExperience = application.YearsOfExperience,
                Area = application.Area,
                Skills = application.Skills,
                CurrentSalary = application.CurrentSalary,
                ExpectedSalary = application.ExpectedSalary,
                CurrentOrganization = application.CurrentOrganization,
                CurrentDesignation = application.CurrentDesignation,
                LastUniversity = application.LastUniversity,
                LastDegree = application.LastDegree,
                Location = application.Location
            };
            return jobApplication;
        }

        public async Task<List<JobApplicationResponseDto>> JobApplications()
        {
            var result = new List<JobApplicationResponseDto>();
            var applications = await _jobApplicationRepository.JobApplications();
            if (applications.HasData())
            {
                foreach (var app in applications)
                {
                    result.Add(new JobApplicationResponseDto() {
                    Id = app.Id,
                    Name = app.Name,
                    Email = app.Email,
                    Phone = app.Phone,
                    CvUrl = app.CvUrl,
                    YearsOfExperience = app.YearsOfExperience,
                    Area = app.Area,
                    Skills = app.Skills,
                    CurrentSalary = app.CurrentSalary,
                    ExpectedSalary = app.ExpectedSalary,
                    CurrentOrganization = app.CurrentOrganization,
                    CurrentDesignation = app.CurrentDesignation,
                    LastUniversity = app.LastUniversity,
                    LastDegree = app.LastDegree,
                    Location = app.Location
                    });
                }
            }
            return result;
        }

        public async Task<bool> SaveJobApplication(JobApplicationRequestDto request)
        {
            if (request is null) return false;
            var jobApplication = new Domain.Data.Domain.JobApplication()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                CvUrl = request.CvUrl,
                YearsOfExperience = request.YearsOfExperience,
                Area = request.Area,
                Skills = request.Skills,
                CurrentSalary = request.CurrentSalary,
                ExpectedSalary = request.ExpectedSalary,
                CurrentOrganization = request.CurrentOrganization,
                CurrentDesignation = request.CurrentDesignation,
                LastUniversity = request.LastUniversity,
                LastDegree = request.LastDegree,
                Location = request.Location ,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };
            var result = await _jobApplicationRepository.SaveJobApplication(jobApplication);
            return true;
        }
        public async Task<bool> SaveJobApplication(JobApplicationDto request)
        {
            if (request is null) return false;
            var jobApplication = new Domain.Data.Domain.JobApplication()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                CvUrl = request.CvUrl,
                YearsOfExperience = request.YearsOfExperience,
                Area = request.Area,
                Skills = request.Skills,
                CurrentSalary = request.CurrentSalary,
                ExpectedSalary = request.ExpectedSalary,
                CurrentOrganization = request.CurrentOrganization,
                CurrentDesignation = request.CurrentDesignation,
                LastUniversity = request.LastUniversity,
                LastDegree = request.LastDegree,
                Location = request.Location,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };
            var result = await _jobApplicationRepository.SaveJobApplication(jobApplication);
            return true;
        }
        public async Task<bool> UpdateJobApplication(JobApplicationUpdateRequestDto request)
        {
            if (request is null) return false;
            var application = await _jobApplicationRepository.JobApplication(request.Id);
            if (application is null) return false;
            
                application.Name = request.Name;
                application.Email = request.Email;
                application.Phone = request.Phone;
                application.YearsOfExperience = request.YearsOfExperience;
                application.Area = request.Area;
                application.Skills = request.Skills;
                application.CurrentSalary = request.CurrentSalary;
                application.ExpectedSalary = request.ExpectedSalary;
                application.CurrentOrganization = request.CurrentOrganization;
                application.CurrentDesignation = request.CurrentDesignation;
                application.LastUniversity = request.LastUniversity;
                application.LastDegree = request.LastDegree;
                application.Location = request.Location;
                
            var result = await _jobApplicationRepository.UpdateJobApplication(application);
            return true;
        }
    }
}
