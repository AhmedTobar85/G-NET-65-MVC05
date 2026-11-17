using AutoMapper;
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
    public class MemberService(IUnitOfWork unitOfWork, IMapper mapper) : IMemberService
    {

        public async Task<IEnumerable<MemberIndexViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var members = await unitOfWork.GetRepository<Member>().GetAllAsync(cancellationToken: ct);

            if (members is null) return [];
            var memberViemModel = mapper.Map<IEnumerable<Member>, IEnumerable<MemberIndexViewModel>>(members);
            return memberViemModel;

        }
        public async Task<bool> CreateMemberAsync(MemberCreateViewModel model, CancellationToken ct = default)
        {

            var emailExist = await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email);
            var phoneExist = await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == model.Phone);

            if (emailExist || phoneExist) return false;
            var member = mapper.Map<Member>(model);
            unitOfWork.GetRepository<Member>().Add(member);
            var result = await unitOfWork.SaveChangesAsync(ct);

            return result > 0;

        }

        public async Task<MemberDetailsViewModel?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var member = await unitOfWork.GetRepository<Member>().GetByIdAsync(id, ct);

            if (member is null) return null;

            var model = mapper.Map<Member, MemberDetailsViewModel>(member);

            var activeMemberShip = await unitOfWork.GetRepository<MemberShip>().FirstOrDefaultAsync(m => m.MemberId == id && m.EndDate > DateTime.UtcNow, cancellationToken: ct);

            if (activeMemberShip is not null)
            {
                var activePlan = await unitOfWork.GetRepository<Plan>().GetByIdAsync(activeMemberShip.Id, ct);
                model.PlanName = activePlan!.Name;
                model.MembershipStartDate = activeMemberShip.CreatedAt.ToString();
                model.MembershipEndDate = activeMemberShip.EndDate.ToString();
            }
            return model;
        }

        public async Task<CreateHealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken ct = default)
        {
            var record = await unitOfWork.GetRepository<HealthRecord>().FirstOrDefaultAsync(h => h.MemberId == id, cancellationToken: ct);
            if (record is null) return null;
            else
                return mapper.Map<HealthRecord, CreateHealthRecordViewModel>(record);


        }

        public async Task<MemberEditViewModel?> GetForUpdateAsync(int id, CancellationToken ct = default)
        {
            var member = await unitOfWork.GetRepository<Member>().GetByIdAsync(id, ct);

            if (member is null) return null;


            return mapper.Map<Member, MemberEditViewModel>(member);
        }

        public async Task<bool> UpdateAsync(int id, MemberEditViewModel model, CancellationToken ct = default)
        {
            var member = await unitOfWork.GetRepository<Member>().GetByIdAsync(id, ct);
            if (member is null) return false;

            var emailExist = await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email && m.Id != id);
            var phoneExist = await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == model.Email && m.Id != id);
            if (emailExist || phoneExist) return false;

            mapper.Map(model, member);
            member.UpdatedAt = DateTime.Now;

            unitOfWork.GetRepository<Member>().Update(member);
            var result = await unitOfWork.SaveChangesAsync(ct);

            return result > 0;



        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {

            var member = await unitOfWork.GetRepository<Member>().GetByIdAsync(id, ct);
            if (member is null) return false;

            var hasFutureBooking = await unitOfWork.GetRepository<Booking>().AnyAsync(b => b.MemberId == id && b.Session.StartDate > DateTime.Now, ct);
            if (hasFutureBooking) return false;



            unitOfWork.GetRepository<Member>().Delete(member);
            var result = await unitOfWork.SaveChangesAsync(ct);
            return result > 0;
        }
    }
}
