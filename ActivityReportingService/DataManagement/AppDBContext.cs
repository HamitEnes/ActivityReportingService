using ActivityReportingService.Models.ActivityManagement;
using Microsoft.EntityFrameworkCore;

namespace ActivityReportingService.DataManagement
{
    /// <summary>
    /// This class manages EF Core context.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Default constructor for AppDbContext
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// DbSet for activity records
        /// </summary>
        public DbSet<Activity> Activities { get; set; }
    }
}
