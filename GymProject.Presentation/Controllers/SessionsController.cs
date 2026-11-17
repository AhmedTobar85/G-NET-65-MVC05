using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymProject.Presentation.Controllers
{
    public class SessionsController(ISessionServices services) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await services.GetAllAsync(ct);
            return View(sessions);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            await PopulateDropDownListAsync(ct);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SessionCreateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownListAsync(ct);
                return View(model);
            }
            var result = await services.CreateAsync(model, ct: ct);
            if (result.success)
            {
                TempData["Success"] = "Session Created";
                return RedirectToAction(nameof(Index));
            }

            TempData["Erorr"] = "Session faild To Create ";
            await PopulateDropDownListAsync(ct);
            return View(model);




        }
        private async Task PopulateDropDownListAsync(CancellationToken ct)
        {
            ViewBag.Trainers = new SelectList(await services.GetTrainersForDropDownAsync(ct), "Id", "Name");
            ViewBag.Categories = new SelectList(await services.GetCategoriesForDropDownAsync(ct), "Id", "Name");
        }
    }
}
