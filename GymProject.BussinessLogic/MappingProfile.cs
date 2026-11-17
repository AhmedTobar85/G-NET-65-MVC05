
using AutoMapper;

using GymProject.BussinessLogic.ViewModels.HealthRecordViewModel;
using GymProject.BussinessLogic.ViewModels.MemberViewModels;
using GymProject.BussinessLogic.ViewModels.SessionViewModel;
using GymProject.DataAccess.Models;
using static GymProject.BussinessLogic.ViewModels.SessionViewModel.SessionIndexViewModel;

namespace GymProject.BussinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapMember();

            MapSession();

        }

        private void MapMember()
        {

            CreateMap<Member, MemberDetailsViewModel>().
                  ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}")).
                  ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));

            CreateMap<Member, MemberIndexViewModel>();

            CreateMap<HealthRecord, CreateHealthRecordViewModel>();

            CreateMap<Member, MemberEditViewModel>().
                ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber)).
                ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City)).
                ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<MemberEditViewModel, Member>().
                ForMember(dest => dest.Name, opt => opt.Ignore()).
                ForMember(dest => dest.Photo, opt => opt.Ignore()).
                AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                });

            CreateMap<MemberCreateViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street

                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord()
                {
                    Weight = src.HealthRecord.Weight,
                    Height = src.HealthRecord.Height,
                    Note = src.HealthRecord.Note,
                    BloodType = src.HealthRecord.BloodType,
                }));

        }
        private void MapSession()
        {
            CreateMap<SessionCreateViewModel, Session>();
            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>();
        }

    }
}
