using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.TrainerViewModel;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;


namespace GymProject.BussinessLogic.Services.Classes
{
    public class TrainerService(IRepository<Trainer> trainerRepo,
                                IRepository<Session> sessionRepo) : ITrainerService
    {
        public async Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var trainers = await trainerRepo.GetAllAsync(cancellationToken:ct);
            return trainers.Select(t => new TrainerIndexViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Specialties = t.Specialty.ToString()
            });
        }
        public async Task<TrainerIndexViewModel?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepo.GetByIdAsync(id, ct);
            if (trainer == null)
                return null;
            else
                return new TrainerIndexViewModel()
                {
                    Name = trainer.Name,
                    Specialties = trainer.Specialty.ToString(),
                    Email = trainer.Email,
                    Phone = trainer.Phone,
                    DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                    Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}"
                };
        }
        public async Task<bool> CreateAsync(TrainerCreateViewModel model, CancellationToken ct = default)
        {
            if (await trainerRepo.AnyAsync(t => t.Email == model.Email, ct))
                return false;
            if (await trainerRepo.AnyAsync(t => t.Phone == model.Phone, ct))
                return false;

            var trainer = new Trainer()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Specialty = model.Specialties,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Address = new Address()
                {
                    City = model.City,
                    BuildingNumber = model.BuildingNumber,
                    Street = model.Street
                }
            };

            var result = await trainerRepo.AddAsync(trainer);
       
            return result > 0;

        }





        public async Task<TrainerUpdateViewModel?> GetToUpdateAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepo.GetByIdAsync(id, ct);
            if (trainer == null)
                return null;
            else
                return new TrainerUpdateViewModel()
                {
                    Name = trainer.Name,
                    Email = trainer.Email,
                    Phone = trainer.Phone,
                    BuildingNumber = trainer.Address.BuildingNumber,
                    Street = trainer.Address.Street,
                    City = trainer.Address.City,
                    Specialties = trainer.Specialty
                };
        }


        public async Task<bool> UpdateDetailsAsync(int trainerId, TrainerUpdateViewModel model, CancellationToken ct = default)
        {
            var trainer = await trainerRepo.GetByIdAsync(trainerId, ct);
            if (trainer is null) return false;

            if (await trainerRepo.AnyAsync(t => t.Email == model.Email && t.Id != trainerId, ct))
                return false;
            if (await trainerRepo.AnyAsync(t => t.Phone == model.Phone && t.Id != trainerId, ct))
                return false;

            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.City = model.City;
            trainer.Address.Street = model.Street;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Specialty = model.Specialties;
            trainer.UpdatedAt = DateTime.Now;
            var result = await trainerRepo.UpdateAsync(trainer);
          
            return result > 0;
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepo.GetByIdAsync(id, ct);
            if (trainer is null) return false;

            var hasFutureSessions = await sessionRepo.AnyAsync(s => s.TrainerId == id && s.StartDate > DateTime.Now, ct);
            if (hasFutureSessions)
                return false;

            var result = await trainerRepo.DeleteAsync(trainer);
          
            return result > 0;
        }
    }
}

