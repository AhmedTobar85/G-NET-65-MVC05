using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Mvc;


namespace GymProject.Presentation.Controllers
{
    public class PlansController(IPlanServices planServices) : Controller
    {


        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planServices.GetAllAsync(ct: ct);
            return View(plans);
        }
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var plan = await planServices.GetByIdAsync(id, ct);
            if (plan is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var plan = await planServices.GetToUpdateAsync(id, ct);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan cannot be edited (not found, inactive, or has active memberships).";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlanUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await planServices.UpdateAsync(id, model, ct);
            if (result)
                TempData["SuccessMessage"] = "Plan updated successfully.";
            else
                TempData["ErrorMessage"] = "Plan Failed To update";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Activate(int id, CancellationToken ct)
        {
            var result = await planServices.ToggleActivationAsync(id, ct);
            if (result)
                TempData["SuccessMessage"] = "Plan status changed";
            else
                TempData["ErrorMessage"] = "Failed to Toggle Plan Status";
            return RedirectToAction(nameof(Index));
        }
    }
}

