using GymProject.BussinessLogic.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.BussinessLogic.Services.Interfaces
{
    public interface IPlanServices
    {
        Task<IEnumerable<PlanIndexViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<PlanIndexViewModel?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<PlanUpdateViewModel?> GetToUpdateAsync(int id, CancellationToken ct = default);
        Task<bool> ToggleActivationAsync(int id, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, PlanUpdateViewModel model, CancellationToken ct = default);
    }
}
