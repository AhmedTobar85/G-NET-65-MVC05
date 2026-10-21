
using GymProject.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GymProject.Controllers
{
    public class PlansController:Controller
    {
        public AppDbContext dbContext = new AppDbContext();
        public async Task<IActionResult> Index()
        {
            var plans = await dbContext.Plans.ToListAsync();
            return View(plans);
        }
        public async Task<IActionResult> Details(int id)
        {
            if(id<=0)
            {
                return NotFound();
            }
            var plan = await dbContext.Plans.FindAsync(id);
            if(plan is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
