using Ostral.Core.Implementations;
using Ostral.Core.Interfaces;
using Ostral.Infrastructure.EmailService;
using Ostral.Infrastructure.Repository;

namespace Ostral.API.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDigitTokenService, DigitTokenService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, SmtpEmailService>();
            
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUserService, UserService>();
                
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IContentRepository, ContentRespository>();
            services.AddScoped<IContentService, ContentService>();
            
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<ITutorService, TutorService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<IStudentCourseService, StudentCourseService>();

            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<ITutorService, TutorService>();
        }
    }
}