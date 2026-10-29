using GymProject.DataAccess.Data.Contexts;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GymProject.Presentation.Controllers
{
    public class PlansController(IRepository<Plan> repo):Controller
    {
   
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await repo.GetAllAsync(cancellationToken:ct);
            return View(plans);
        }
        public async Task<IActionResult> Details(int id)
        {
            if(id<=0)
            {
                return NotFound();
            }
            var plan = await repo.GetByIdAsync(id);
            if(plan is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
