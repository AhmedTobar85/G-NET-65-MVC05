using AutoMapper;
using GymProject.BussinessLogic.Common;
using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.SessionViewModel;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Models.Enums;
using GymProject.DataAccess.Repositories.Interfaces;
using static GymProject.BussinessLogic.ViewModels.SessionViewModel.SessionIndexViewModel;

namespace GymProject.BussinessLogic.Services.Classes
{
    public class SessionServices(ISessionRepository sessionRepo,
                                IUnitOfWork unitOfWork,
                                IMapper mapper) : ISessionServices
    {

        public async Task<IEnumerable<SessionIndexViewModel>?> GetAllAsync(CancellationToken ct)
        {
            var sessions = await sessionRepo.GetAllSessionsWithTrainerAndCategory(ct);
            if (sessions == null || !sessions.Any()) return null;
            var mappedSessions = sessions.Select(s => new SessionIndexViewModel()
            {
                Id = s.Id,
                Capacity = s.Capacity,
                Category = s.Category.Name,
                Description = s.Description,
                Trainer = s.Trainer.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,



            });

            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - await sessionRepo.GetCountOfBookedSlotsAsync(session.Id, ct);
            }
            return mappedSessions;
        }

        public async Task<Result> CreateAsync(SessionCreateViewModel model, CancellationToken ct = default)
        {
            if (model.EndDate <= model.StartDate) return Result.Validation("EndDate Must Be After StartDate");
            if (model.StartDate <= DateTime.Now) return Result.Validation("StartDate Must Be After In The Future");
            if (model.Capacity < 1 || model.Capacity > 25) return Result.Validation("Capacity Must Be Beetwen 1 and 25");

            var trainer = await unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer is null) return Result.NotFound("Trainer Not Found");
            var category = await unitOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId);
            if (category is null) return Result.NotFound("category Not Found");

            var isValid = Enum.TryParse<Specialty>(category.Name, true, out var categorySpecialty);
            if (!isValid || trainer.Specialty != categorySpecialty) return Result.Validation("Can Not Create This Session To The Trainer");
            var session = mapper.Map<Session>(model);
            unitOfWork.GetRepository<Session>().Add(session);
            var result = await unitOfWork.SaveChangesAsync(ct);
            return Result.Ok();
        }

        public async Task<IEnumerable<TrainerSelectViewModel>> GetTrainersForDropDownAsync(CancellationToken ct)
        {

            var trainers = await unitOfWork.GetRepository<Trainer>().GetAllAsync(cancellationToken: ct);
            return mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        public async Task<IEnumerable<CategorySelectViewModel>> GetCategoriesForDropDownAsync(CancellationToken ct)
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(cancellationToken: ct);
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategorySelectViewModel>>(categories);
        }
    }
}
