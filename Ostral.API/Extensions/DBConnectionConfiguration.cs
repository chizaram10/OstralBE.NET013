using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ostral.ConfigOptions;
using Ostral.Core.Implementations;
using Ostral.Domain.Models;
using Ostral.Infrastructure;

namespace Ostral.API.Extensions
{
    public static class DBConnectionConfiguration
    {
        private static string GetHerokuConnectionString()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL")!;
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
                   $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        public static void AddDbContextExtension(this IServiceCollection services, IWebHostEnvironment env,
            IConfiguration config)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            
            var connectionStrings = new ConnectionStrings();
            
            services.AddDbContextPool<OstralDBContext>(options =>
            {
                if (env.IsProduction())
                {
                    connectionStrings.DefaultConnection = Environment.GetEnvironmentVariable("ConnectionString")!;
                }
                else
                {
                    config.Bind(nameof(connectionStrings), connectionStrings);
                }

                options.UseNpgsql(connectionStrings.DefaultConnection);
            });

            var builder = services.AddIdentity<User, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 8;
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.User.RequireUniqueEmail = true;
                x.SignIn.RequireConfirmedEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            _ = builder.AddEntityFrameworkStores<OstralDBContext>()
                .AddTokenProvider<DigitTokenService>(DigitTokenService.DIGITEMAIL)
                .AddDefaultTokenProviders();
        }
    }
}