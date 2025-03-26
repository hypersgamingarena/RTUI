using Microsoft.EntityFrameworkCore;
using RTUI.Models;

namespace RTUI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet to represent the "Projects" table
        public DbSet<Project> Projects { get; set; }
    }
}
