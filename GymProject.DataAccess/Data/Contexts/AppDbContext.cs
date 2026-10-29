using GymProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GymProject.DataAccess.Data.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberShip> Memberships { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
  
    }
}
