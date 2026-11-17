using GymProject.BussinessLogic.Common;
using GymProject.BussinessLogic.ViewModels.SessionViewModel;
using static GymProject.BussinessLogic.ViewModels.SessionViewModel.SessionIndexViewModel;

namespace GymProject.BussinessLogic.Services.Interfaces
{
    public interface ISessionServices
    {
        Task<IEnumerable<SessionIndexViewModel>?> GetAllAsync(CancellationToken ct = default);
        Task<Result> CreateAsync(SessionCreateViewModel model, CancellationToken ct = default);
        Task<IEnumerable<TrainerSelectViewModel>> GetTrainersForDropDownAsync(CancellationToken ct);
        Task<IEnumerable<CategorySelectViewModel>> GetCategoriesForDropDownAsync(CancellationToken ct);
    }
}
