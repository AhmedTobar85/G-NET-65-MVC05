using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.PlanViewModel;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GymProject.BussinessLogic.Services.Classes
{
    public class PlanServices(IUnitOfWork unitOfWork) : IPlanServices
    {
        public async Task<IEnumerable<PlanIndexViewModel>> GetAllAsync(CancellationToken ct = default)
        {

            var plans = await unitOfWork.GetRepository<Plan>().GetAllAsync(cancellationToken: ct);
            return plans.Select(p => new PlanIndexViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
                Price = p.Price
            });
        }

        public async Task<PlanIndexViewModel?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var plan = await unitOfWork.GetRepository<Plan>().GetByIdAsync(id, ct);
            if (plan is null)
                return null;
            else
                return new PlanIndexViewModel()
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    Price = plan.Price,
                    DurationDays = plan.DurationDays,
                    IsActive = plan.IsActive
                };
        }

        public async Task<PlanUpdateViewModel?> GetToUpdateAsync(int id, CancellationToken ct = default)
        {
            var plan = await unitOfWork.GetRepository<Plan>().GetByIdAsync(id, ct);
            if (plan is null || !plan.IsActive) return null;
            if (await HasActiveMembershipsAsync(id, ct))
                return null;
            else
                return new PlanUpdateViewModel()
                {
                    Name = plan.Name,
                    Price = plan.Price,
                    DurationDays = plan.DurationDays,
                    Description = plan.Description
                };
        }

        public async Task<bool> ToggleActivationAsync(int planId, CancellationToken ct = default)
        {
            var plan = await unitOfWork.GetRepository<Plan>().GetByIdAsync(planId, ct);
            if (plan is null) return false;

            if (plan.IsActive && await HasActiveMembershipsAsync(planId, ct))
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.Now;
            unitOfWork.GetRepository<Plan>().Update(plan);
            var result = await unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(int id, PlanUpdateViewModel model, CancellationToken ct = default)
        {
            var plan = await unitOfWork.GetRepository<Plan>().GetByIdAsync(id, ct);
            if (plan is null) return false;
            if (await HasActiveMembershipsAsync(id, ct))
                return false;

            plan.DurationDays = model.DurationDays;
            plan.Price = model.Price;
            plan.Description = model.Description;
            plan.UpdatedAt = DateTime.Now;
            unitOfWork.GetRepository<Plan>().Update(plan);
            var result = await unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }

        private async Task<bool> HasActiveMembershipsAsync(int planId, CancellationToken ct)
        {
            return await unitOfWork.GetRepository<MemberShip>().AnyAsync(m => m.PlanId == planId && m.EndDate > DateTime.Now, ct);
        }

    }
}
