using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymProject.Presentation.Controllers
{
    public class TrainersController(ITrainerService service) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            return View(await service.GetAllAsync(ct));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await service.CreateAsync(model, ct);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer created successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Trainer Failed create";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var trainer = await service.GetDetailsAsync(id, ct);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var trainer = await service.GetToUpdateAsync(id, ct);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrainerUpdateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await service.UpdateDetailsAsync(id, model, ct);
            if (result)
                TempData["SuccessMessage"] = "Trainer updated successfully.";
            else
                TempData["ErrorMessage"] = "Trainer Failed To update";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var trainer = await service.GetDetailsAsync(id, ct);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var result = await service.RemoveAsync(id, ct);
            if (result)
                TempData["SuccessMessage"] = "Trainer deleted successfully.";
            else
                TempData["ErrorMessage"] = "Failed To delete Trainer";
            return RedirectToAction(nameof(Index));
        }
    }
}
