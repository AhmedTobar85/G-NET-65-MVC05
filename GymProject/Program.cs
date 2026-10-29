using GymProject.DataAccess.Data.Contexts;
using GymProject.DataAccess.Data.Seeder;
using GymProject.DataAccess.Repositories.Classes;
using GymProject.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();


app.UseRouting();



app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

await DatabaseSeeder.SeedAllAsync(dbContext);

app.Run();
