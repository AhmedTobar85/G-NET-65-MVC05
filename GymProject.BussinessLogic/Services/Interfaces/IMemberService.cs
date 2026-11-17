using GymProject.BussinessLogic.ViewModels.HealthRecordViewModel;
using GymProject.BussinessLogic.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.BussinessLogic.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberIndexViewModel>> GetAllMemberAsync(CancellationToken ct = default);
        public Task<bool> CreateMemberAsync(MemberCreateViewModel model, CancellationToken ct = default);
        public Task<MemberDetailsViewModel?> GetDetailsAsync(int id, CancellationToken ct = default);
        public Task<CreateHealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken ct = default);

        public Task<MemberEditViewModel?> GetForUpdateAsync(int id, CancellationToken ct = default);
        public Task<bool> UpdateAsync(int id, MemberEditViewModel model, CancellationToken ct = default);
        public Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}