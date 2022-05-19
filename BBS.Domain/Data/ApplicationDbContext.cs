using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CVBank.Domain.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using BBS.Domain.Data.Domain;

namespace CVBank.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
