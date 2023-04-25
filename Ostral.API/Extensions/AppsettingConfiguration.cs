using Ostral.ConfigOptions;

namespace Ostral.API.Extensions;

public static class AppsettingConfiguration
{
    
    public static void AddAppSettingsConfig(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {
        var jwt = new Jwt();
        var mailSettings = new MailSettings();
        var cloudinarySettings = new CloudinarySettings();

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

            cloudinarySettings.ApiKey = Environment.GetEnvironmentVariable("CloudinaryApiKey")!;
            cloudinarySettings.ApiSecret = Environment.GetEnvironmentVariable("CloudinaryApiSecret")!;
            cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("CloudinaryCloudName")!;
        }
        else
        {
            config.Bind(nameof(jwt), jwt);
            config.Bind(nameof(mailSettings), mailSettings);
            config.Bind(nameof(cloudinarySettings), cloudinarySettings);
        }
        
        services.AddSingleton(jwt);
        services.AddSingleton(mailSettings);
    } 
}