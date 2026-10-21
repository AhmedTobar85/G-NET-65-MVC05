using GymProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data.Contexts
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=GymCore; Integrated Security=SSPI; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Plan> Plans { get; set; }
    }
}
