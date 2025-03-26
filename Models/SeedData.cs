using Microsoft.EntityFrameworkCore;
using RTUI.Models;

namespace RTUI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // If the database is already populated, skip the seeding process
            if (context.Projects.Any()) return;

            // Seed data for Projects
            context.Projects.AddRange(
                new Project { Name = "AI Debugging Tool", Description = "Fixes and enhances website code automatically.", Status = "In Progress" },
                new Project { Name = "E-Commerce Web App", Description = "A feature-rich e-commerce solution.", Status = "Completed" },
                new Project { Name = "Mobile Companion App", Description = "Syncs with web app for mobile support.", Status = "Pending" }
            );

            context.SaveChanges();
        }
    }
}
