using GymProject.Data.Contexts;
using GymProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Data.Seeder
{
    public static class PlanSeeder
    {
        public static async Task SeedAsync()
        {
            using var dbContext = new AppDbContext();

            bool hasAnyPlans = await dbContext.Plans.AnyAsync();

            if (hasAnyPlans)
            {
                return;
            }
            List<Plan> plans =
                      [
                          new ()
                        {
                            Name = "Basic",
                            Description = "Basic gym access",
                            DurationDays = 30,
                            Price = 300,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        },
                       new()
                        {
                            Name = "Silver",
                            Description = "Gym access with Cardio classes",
                            DurationDays = 60,
                            Price = 500,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            Name = "Gold",
                            Description = "Full gym access with trainer",
                            DurationDays = 90,
                            Price = 900,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        }
            ];

            await dbContext.Plans.AddRangeAsync(plans);

            await dbContext.SaveChangesAsync();
        }
    }
}
