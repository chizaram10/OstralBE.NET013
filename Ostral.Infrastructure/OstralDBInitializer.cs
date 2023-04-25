using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ostral.Domain.Models;

namespace Ostral.Infrastructure
{
    public class OstralDBInitializer
    {
        public readonly static string DATAPATH = @"Ostral.Infrastructure\Data\";

        public static async Task Seed(IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<OstralDBContext>();
            var currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            if (currentDirectory == null)
                return;
            string filePath = Path.Combine(currentDirectory.FullName, DATAPATH);
            if (context == null || await context.Database.EnsureCreatedAsync())
                return;

			// JSON seeding of data to be written here

			if (!context.Tutors.Any())
			{
				string fileName = Path.Combine(filePath, "tutor.json");
				var read = File.ReadAllText(fileName);
				var data = JsonConvert.DeserializeObject<Tutor>(read);
					await context.Tutors.AddAsync(data!);
				await context.SaveChangesAsync();
			}
			if (!context.Courses.Any())
			{
				string fileName = Path.Combine(filePath, "courses.json");
				var read = File.ReadAllText(fileName);
				var data = JsonConvert.DeserializeObject<List<Course>>(read);
					await context.Courses.AddRangeAsync(data!);
				await context.SaveChangesAsync();
			}
			if (!context.Students.Any())
			{
				string fileName = Path.Combine(filePath, "students.json");
				var read = File.ReadAllText(fileName);
				var data = JsonConvert.DeserializeObject<List<Student>>(read);
				foreach (var item in data!)
					await context.Students.AddAsync(item);
				await context.SaveChangesAsync();
			}
            if (!context.Contents.Any())
            {
                string fileName = Path.Combine(filePath, "contents.json");
                var read = File.ReadAllText(fileName);
                var data = JsonConvert.DeserializeObject<List<Content>>(read);
                foreach (var item in data!)
                    await context.Contents.AddAsync(item!);
                await context.SaveChangesAsync();
            }
        }
    }
}
