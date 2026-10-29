using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymProject.Presentation.Controllers
{
    public class MembersController(IMemberService service) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await service.GetAllMemberAsync(ct);
            return View(members);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberCreateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await service.CreateMemberAsync(model, ct);
            if (result)
                TempData["Success"] = "Member Created Successfully";
            else
                TempData["Error"] = "Faild To Create Member";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var member = await service.GetDetailsAsync(id, ct);
            if (member is null)
            {
                TempData["Error"] = "Member Is Not Found ";
                return RedirectToAction(nameof(Index));

            }
            return View(member);

        }
        public async Task<IActionResult> HealthRecord(int id, CancellationToken ct)
        {
            var healthRecord = await service.GetHealthRecordAsync(id, ct);
            if (healthRecord is null)
            {
                TempData["Error"] = "Health Record is Not Found";
                return RedirectToAction(nameof(Index));

            }

            return View(healthRecord);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var member = await service.GetForUpdateAsync(id, ct);
            if (member is null)
            {
                TempData["Error"] = "Member Is Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, MemberEditViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await service.UpdateAsync(id, model, ct);
            if (result)
                TempData["Success"] = "Member Updated Successfully";
            else
                TempData["Error"] = "Faild To Update Member ";

            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var member = await service.GetDetailsAsync(id, ct);
            if (member is null)
            {
                TempData["Error"] = "Member Is Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        {
            var result = await service.DeleteAsync(id, ct);
            if (result)
                TempData["Success"] = "Member Deleted Successfully";
            else
                TempData["Error"] = "Member To delete Member";


            return RedirectToAction(nameof(Index));
        }

    }
}
