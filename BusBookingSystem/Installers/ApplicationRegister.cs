using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CVBank.AzureDataLakeService;
using CVBank.Data;
using CVBank.Domain.Data.Domain;
using CVBank.InfraStructure.Repository.File;
using CVBank.InfraStructure.Repository.User;
using CVBank.InfraStructure.Service.File;
using CVBank.InfraStructure.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBank.InfraStructure.Repository.Application;
using CVBank.InfraStructure.Service.JobApplication;

namespace CVBank.Installers
{
    public static class ApplicationRegister
    {
        public static void RegisterResource(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDataLakeService, DataLakeService>(); 
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IJobApplicationService, JobApplicationService>();
            services.AddTransient<IJobApplicationRepository, JobApplicationRepository>();
        }
        public static void RegisterIdentity(this IServiceCollection services)
        {
             services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 7;
                 options.Password.RequireUppercase = true;

                 options.User.RequireUniqueEmail = true;
             })
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();
        }
        public static void RegisterExternalLogin(this IServiceCollection services)
        {
            services.AddAuthentication()
           .AddGoogle(opts =>
           {
               opts.ClientId = "516075189886-k1n0s10igemhq6tpfmkp1641p7bvi6jq.apps.googleusercontent.com";
               opts.ClientSecret = "qtyWJd-jSPw_LQalCdtNU6ju";
               opts.SignInScheme = IdentityConstants.ExternalScheme;
           });
        }
    }
}
