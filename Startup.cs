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
using TopProjectITI_int40.Repository.AdminRepo.SubjectRepositories;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterPhonesRepositories;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSoicalLinkRepositories;

using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterSubjectsRepositories;
using TopProjectITI_int40.Repository.AdminRepo.Category;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherEductionRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherExperienceRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherPhonesRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSchoolRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSocialLinkRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherSubjectRepositories;
using TopProjectITI_int40.Repository.TeacherRepo.TeacherRegisterRepositories;
using TopProjectITI_int40.Models;
using TopProjectITI_int40.Repository.ParentRepo.ParentRepositories;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterGroupRepositories;
using TopProjectITI_int40.Repository.EductionalCenterRepo.EductionalCenterRepositories;
using TopProjectITI_int40.Repository.StudentRepo.StudentRepositories;
using TopProjectITI_int40.Repository.StudentRepo.StudentsGroupsRepositories;
using TopProjectITI_int40.Repository.StudentRepo.StudentSkillsRepositories;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TopProjectITI_int40.Repository.ReportRepo.ReportReporitories.ReportSupRepositories;
using System.Runtime;
using CustomAuthorizeFilterAspNetCore21.Authorization;
using Microsoft.AspNetCore.Authorization;

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


        //Auth Service >>  Registeration in Start up

        private void SetupJWTServices(IServiceCollection services)
        {
            string key = Configuration.GetSection("JwtConfig").GetSection("secret").Value; //this should be same which is used while creating token     
            var issuer = "http://localhost:6853";  //this should be same which is used while creating token 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = issuer,
                       ValidAudience = issuer,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //photo   register for photoSettings
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            //                                                          .UseLazyLoadingProxies().UseSqlServer
            services.AddDbContext<DBGProjectITI_Int40>(option => option.UseSqlServer(Configuration.GetConnectionString("topprojectcon")));

            //// ===== Add Identity ========
            //services.AddIdentity<DBGProjectITI_Int40, IdentityRole>()

            // call function of JWTAuth
            SetupJWTServices(services);


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

            // Custom Authorization
            services.AddControllers();
            //options => options.Filters.Add(new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()))

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
            //         4-  Group
            services.AddScoped<IEductionalCenterGroupRepository, EductionalCenterGroupRepository>();
            //         5-  EductionalCenter
            services.AddScoped<IEductionalCenterRepository, EductionalCenterRepository>();

            // Parent
            //         1 - Register
            services.AddScoped<IParentRepository, ParentRepository>();

            // Student
            //         1 - Register Student
            services.AddScoped<IStudentRepository, StudentRepository>();
            //         2 - Student Group
            services.AddScoped<IStudentsGroupsRepository, StudentsGroupsRepository>();
            //         3 - Student Skills
            services.AddScoped<IStudentSillsRepository, StudentSkillsRepository>();

            // Reports
            //         1- Report
            services.AddScoped<IReportRepository, ReportRepository>();
            //         2- SubRepot
            services.AddScoped<IReportSupRepository, ReportSupRepository>();

            // 

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

            // for Authentication and Authorizations
            app.UseAuthentication();
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
