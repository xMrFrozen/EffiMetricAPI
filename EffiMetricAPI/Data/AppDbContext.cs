using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using EffiMetricAPI.Models;

namespace EffiMetricAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkTask>()
                .HasOne<Employee>()
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.employeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
