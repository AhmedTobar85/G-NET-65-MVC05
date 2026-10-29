using GymProject.BussinessLogic.Services.Interfaces;
using GymProject.BussinessLogic.ViewModels.HealthRecordViewModel;
using GymProject.BussinessLogic.ViewModels.MemberViewModels;
using GymProject.DataAccess.Models;
using GymProject.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.BussinessLogic.Services.Classes
{
    public class MemberService(IRepository<Member> memberRepo,
                               IRepository<MemberShip> memberShipRepo,
                               IRepository<Plan> planRepo,
                               IRepository<HealthRecord> healthRecordnRepo,
                                IRepository<Booking> bookingRepo) : IMemberService
    {
     
        public async Task<IEnumerable<MemberIndexViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var members = await memberRepo.GetAllAsync(cancellationToken: ct);

            if (members is null) return [];
            var memberViemModel = members.Select(m => new MemberIndexViewModel()
            {
                Name = m.Name,
                Gender = m.Gender.ToString(),
                Email=m.Email,
                Phone=m.Phone,
                Photo=m.Photo,
                Id=m.Id,

            });
            return memberViemModel;

                
        }
        public async Task<bool> CreateMemberAsync(MemberCreateViewModel model, CancellationToken ct = default)
        {

            var emailExist = await memberRepo.AnyAsync(m => m.Email == model.Email);
            var phoneExist = await memberRepo.AnyAsync(m => m.Phone == model.Phone);

            if (emailExist || phoneExist) return false;
            var member = new Member()
            {
                Name= model.Name,
                Email= model.Email,
                Phone=model.Phone,
                DateOfBirth=model.DateOfBirth,
                Gender=model.Gender,
                Address=new Address
                {
                    City=model.City,
                    BuildingNumber=model.BuildingNumber,
                    Street =model.Street
                },
                HealthRecord=new HealthRecord()
                {
                    BloodType= model.HealthRecord.BloodType,
                    Note=model.HealthRecord.Note,
                    Height= model.HealthRecord.Height,
                    Weight= model.HealthRecord.Weight,
                }
                
              
                


            };

            var result = await memberRepo.AddAsync(member) ;
     

            return result > 0;
        
        }

        public async Task<MemberDetailsViewModel?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepo.GetByIdAsync(id, ct);

            if (member is null) return null;

            var model = new MemberDetailsViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Gender = member.Gender.ToString(),
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",

                DateOfBirth = member.DateOfBirth.ToShortDateString()
            };
            var activeMemberShip = await memberShipRepo.FirstOrDefaultAsync(m => m.MemberId == id && m.EndDate>DateTime.UtcNow,cancellationToken:ct);

            if(activeMemberShip is not null)
            {
                var activePlan = await planRepo.GetByIdAsync(activeMemberShip.Id,ct);
                model.PlanName = activePlan!.Name;
                model.MembershipStartDate = activeMemberShip.CreatedAt.ToString();
                model.MembershipEndDate = activeMemberShip.EndDate.ToString();
            }
            return model;
        }

        public async Task<CreateHealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken ct = default)
        {
            var record = await healthRecordnRepo.FirstOrDefaultAsync(h => h.MemberId == id);
            if (record is null) return null;
           
            return new CreateHealthRecordViewModel
            {
                Height=record.Height,
                Weight=record.Weight,
                BloodType=record.BloodType,
                Note=record.Note

            };
        }

        public async Task<MemberEditViewModel?> GetForUpdateAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepo.GetByIdAsync(id, ct);

            if (member is null) return null;

            return new MemberEditViewModel
            {
                Photo=member.Photo,
                Name=member.Name,
                Email=member.Email,
                Phone=member.Phone,
                City=member.Address.City,
                Street=member.Address.Street,
                BuildingNumber=member.Address.BuildingNumber,


            };
        }

        public async Task<bool> UpdateAsync( int id, MemberEditViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepo.GetByIdAsync(id, ct);
            if (member is null) return false;

         var emailExist = await memberRepo.AnyAsync(m => m.Email == model.Email && m.Id != id);
         var phoneExist = await memberRepo.AnyAsync(m => m.Email == model.Email && m.Id != id);
            if (emailExist || phoneExist) return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.City = model.City;
            member.Address.Street = model.Street;
            member.Address.BuildingNumber = model.BuildingNumber;

            var result = await memberRepo.UpdateAsync(member);
      
            return result > 0;
         


        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {

            var member = await memberRepo.GetByIdAsync(id, ct);
            if (member is null) return false;

            var hasFutureBooking = await bookingRepo.AnyAsync(b => b.MemberId == id && b.Session.StartDate > DateTime.Now,ct);
            if (hasFutureBooking) return false;



            var result = await memberRepo.DeleteAsync(member);
      
            return result > 0;
        }
    }
}
