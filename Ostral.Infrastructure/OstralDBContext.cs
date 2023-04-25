using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ostral.Domain.Models;
using System.Reflection;
using System.Reflection.Emit;

namespace Ostral.Infrastructure
{
    public class OstralDBContext : IdentityDbContext<User>
    {
        private const string UPDATEDAT = "UpdatedAt";
        private const string CREATEDAT = "CreatedAt";

        public OstralDBContext(DbContextOptions<OstralDBContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
            
			builder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
			builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CourseList)
                .HasForeignKey(sc => sc.StudentId);

			builder.Entity<StudentCourse>()
				.HasOne(sc => sc.Course)
				.WithMany(c => c.StudentList)
				.HasForeignKey(sc => sc.CourseId);

			base.OnModelCreating(builder);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is Property appUser)
                {
                    AuditPropertiesChange(item.State, appUser.GetType());
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public static void AuditPropertiesChange<T>(EntityState state, T obj) where T : class
        {
            PropertyInfo? value;
            switch (state)
            {
                case EntityState.Modified:
                    value = obj.GetType().GetProperty(UPDATEDAT);
                    value?.SetValue(obj, DateTime.UtcNow);
                    break;
                case EntityState.Added:
                    value = obj.GetType().GetProperty(CREATEDAT);
                    value?.SetValue(obj, DateTime.UtcNow);
                    value = obj.GetType().GetProperty(UPDATEDAT);
                    value?.SetValue(obj, DateTime.UtcNow);
                    break;
                default:
                    break;
            }
        }
	}
}