using Ostral.Domain.Enums;

namespace Ostral.API.Extensions
{
    public static class AuthorizationConfiguration
    {
        public static void AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireTutorOnly", policy => policy.RequireRole(UserRole.Tutor.ToString()));
                options.AddPolicy("RequireStudentOnly", policy => policy.RequireRole(UserRole.Student.ToString()));
                options.AddPolicy("RequireTutorAndStudent", policy =>
                {
                    policy.RequireRole(UserRole.Tutor.ToString());
                    policy.RequireRole(UserRole.Student.ToString());
                });
            });
        }
    }
}