using Ostral.ConfigOptions;

namespace Ostral.API.Extensions;

public static class AppsettingConfiguration
{
    
    public static void AddAppSettingsConfig(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {
        var jwt = new Jwt();
        var mailSettings = new MailSettings();

        if (env.IsProduction())
        {
            jwt.Token = Environment.GetEnvironmentVariable("JwtToken")!;
            jwt.Issuer = Environment.GetEnvironmentVariable("JwtIssuer")!;
            jwt.Audience = Environment.GetEnvironmentVariable("JwtAudience")!;
            jwt.LifeTime = double.Parse(Environment.GetEnvironmentVariable("JwtLifeTime")!);

            mailSettings.Host = Environment.GetEnvironmentVariable("MailHost")!;
            mailSettings.Port = int.Parse(Environment.GetEnvironmentVariable("MailPort")!);
            mailSettings.DisplayName = Environment.GetEnvironmentVariable("MailDisplayName")!;
            mailSettings.Username = Environment.GetEnvironmentVariable("MailUsername")!;
            mailSettings.Password = Environment.GetEnvironmentVariable("MailPassword")!;
        }
        else
        {
            config.Bind(nameof(jwt), jwt);
            config.Bind(nameof(mailSettings), mailSettings);
        }
        
        services.AddSingleton(jwt);
        services.AddSingleton(mailSettings);
    } 
}