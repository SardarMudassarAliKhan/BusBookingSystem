using CVBank.Data;
using CVBank.Domain.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Repository.Application
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplication> JobApplication(long applicationId = 0)
        {
            return await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == applicationId);
        }

        public async Task<List<JobApplication>> JobApplications()
        {
            return await _context.JobApplications.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<JobApplication> SaveJobApplication(JobApplication request)
        {
            if (request is null) return null;
            _context.JobApplications.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<JobApplication> UpdateJobApplication(JobApplication request)
        {
            if (request is null) return null;
            _context.JobApplications.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
