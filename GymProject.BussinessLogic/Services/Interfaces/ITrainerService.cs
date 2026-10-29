using GymProject.BussinessLogic.ViewModels.TrainerViewModel;

namespace GymProject.BussinessLogic.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<TrainerIndexViewModel?> GetDetailsAsync(int id, CancellationToken ct = default);
        Task<TrainerUpdateViewModel?> GetToUpdateAsync(int id, CancellationToken ct = default);
        Task<bool> CreateAsync(TrainerCreateViewModel model, CancellationToken ct = default);
        Task<bool> UpdateDetailsAsync(int trainerId, TrainerUpdateViewModel model, CancellationToken ct = default);
        Task<bool> RemoveAsync(int id, CancellationToken ct = default);
    }
}
