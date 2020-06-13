using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TopProjectITI_int40.AppDBContext;
using TopProjectITI_int40.Repository.Admin.SubjectRepositories;
using TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterPhonesRepositories;
using TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterSoicalLinkRepositories;
using TopProjectITI_int40.Repository.EductionalCenter.EductionalCenterSubjectsRepositories;
using TopProjectITI_int40.Repository.Parent.Category;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherExperienceRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherPhonesRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSchoolRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSocialLinkRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSubjectRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories;
using TopProjectITI_int40.Models;

namespace TopProjectITI_int40
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        string coretest = "test";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //photo
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            //                                                          .UseLazyLoadingProxies().UseSqlServer
            services.AddDbContext<DBGProjectITI_Int40>(option => option.UseSqlServer(Configuration.GetConnectionString("topprojectcon")));

            //// ===== Add Identity ========
            //services.AddIdentity<DBGProjectITI_Int40, IdentityRole>()
            
            //.AddDefaultTokenProviders();
            // use here lazy loading
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerDocument();

            //cors
            services.AddCors(option =>
            {
                option.AddPolicy(coretest,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.AddControllers();
            //Admin   >> 1- Category Subjects hhhh
            services.AddScoped<ICategorySubjectRepository, CategorySubjectRepository>();
            //        >> 2- Subjects
            services.AddScoped<ISubjectRepository, SubjectRepository>();

            //Teacher >> 
            //        >> Teacher Register
            services.AddScoped<ITeacherRegisterRepository, TeacherRegisterRepository>();
            //        >> 1- TeacherSubejects
            services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
            //        >> 2- TeacherPhones
            services.AddScoped<ITeacherPhonesRepository, TeacherPhonesRepository>();
            //        >> 3- TeacherExperiences
            services.AddScoped<ITeacherExperienceRepository, TeacherExperienceRepository>();
            //        >> 4- TeacherSocialLinks
            services.AddScoped<ITeacherSocialLinkRepository, TeacherSocialLinkRepository>();
            //        >> 5- TeacherSchools
            services.AddScoped<ITeacherSchoolRepository, TeacherSchoolRepository>();
            //        >> 6- ITeacherEductions
            services.AddScoped<ITeacherEductionRepository, TeacherEductionRepository>();

            //Eductional Center
            //         1 - EductionalCenterPhones
            services.AddScoped<IEductionalCenterPhoneRepository, EductionalCenterPhoneRepository>();
            //         2-  EductionalCenterSubjects
            services.AddScoped<IEductionalCenterSubjectRepository, EductionalCenterSubjectRepository>();
            //         3-  EductionalCenterSoicalLinks
            services.AddScoped<IEductionalCenterSoicalLinkRepository, EductionalCenterSoicalLinkRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            //use core
            app.UseCors(coretest);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}