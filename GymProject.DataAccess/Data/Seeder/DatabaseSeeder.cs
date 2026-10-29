using GymProject.DataAccess.Data.Contexts;

namespace GymProject.DataAccess.Data.Seeder
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAllAsync(AppDbContext dbContext)
        {
            await PlanSeeder.SeedAsync(dbContext);
        }
    }
}
