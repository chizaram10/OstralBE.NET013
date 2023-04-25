using CloudinaryDotNet;
using Ostral.ConfigOptions;
using Ostral.Domain.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Ostral.API.Extensions
{
	public static class CloudinaryConfiguration
	{
		public static void AddCloudinaryExtention(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
		{
			var cloudinarySettings = new CloudinarySettings();

			if (env.IsProduction())
			{
				cloudinarySettings.ApiKey = Environment.GetEnvironmentVariable("CloudinaryApiKey")!;
				cloudinarySettings.ApiSecret = Environment.GetEnvironmentVariable("CloudinaryApiSecret")!;
				cloudinarySettings.CloudName = Environment.GetEnvironmentVariable("CloudinaryCloudName")!;
			}
			else
			{
                configuration.Bind(nameof(cloudinarySettings), cloudinarySettings);
            };
			
			var cloudinary = new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
			services.AddSingleton(cloudinary);
		}
	}
}
