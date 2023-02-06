using AppApi.DTOs;
using AppApi.Entities;
using AutoMapper;

namespace AppApi.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, UserEntity>();

            CreateMap<UserEntity, UserToReturnDto>()
                .ForMember(
                    dest => dest.Name,
                    options => options.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Role,
                    options => options.MapFrom(src => src.Role.Name)
                );

            CreateMap<UserForUpdateDto, UserEntity>();
            CreateMap<TaxisnetUserDto, UserToReturnDto>();

            CreateMap<UserBill, UserBillToReturnDto>()
                .ForMember(
                    dest => dest.Id,
                    options => options.MapFrom(src => src.Id.ToString())
                );

            CreateMap<UserBillToCreateDto, UserBill>();

            CreateMap<UserBill, BillInfoForAdminToReturnDto>()
                .ForMember(
                    dest => dest.User,
                    options => options.MapFrom(src => src.UserEntity)
                );
        }
    }
}