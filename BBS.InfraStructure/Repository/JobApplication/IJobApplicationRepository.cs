using CVBank.Domain.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Repository.Application
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> SaveJobApplication(JobApplication request);
        Task<JobApplication> UpdateJobApplication(JobApplication request);
        Task<List<JobApplication>> JobApplications();
        Task<JobApplication> JobApplication(long applicationId);

    }
}
