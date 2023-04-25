using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ostral.ConfigOptions;

namespace Ostral.API.Extensions
{
    public static class AuthenticationConfiguration
    {
        public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
        {
            var jwt = new Jwt();
            
            if (env.IsProduction())
            {
                jwt.Token = Environment.GetEnvironmentVariable("JwtToken")!;
                jwt.Issuer = Environment.GetEnvironmentVariable("JwtIssuer")!;
                jwt.Audience = Environment.GetEnvironmentVariable("JwtAudience")!;
                jwt.LifeTime = double.Parse(Environment.GetEnvironmentVariable("JwtLifeTime")!);
            }
            else
            {
                config.Bind(nameof(jwt), jwt);
            }
            
            services.AddAuthentication(v =>
            {
                v.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                v.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(v =>
             {
                 v.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = jwt.Issuer != null,
                     ValidateLifetime = true,
                     ValidateAudience = jwt.Audience != null,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwt.Issuer ?? null,
                     ValidAudience = jwt.Audience ?? null,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Token ?? string.Empty))
                 };
             });
        }
    }
}