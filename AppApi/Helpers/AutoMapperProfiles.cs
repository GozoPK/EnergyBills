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
                    options => options.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserEntity, AccountToReturnDto>();
            CreateMap<TaxisnetUserDto, AccountToReturnDto>();

            CreateMap<UserBill, UserBillToReturnDto>()
                .ForMember(
                    dest => dest.Id,
                    options => options.MapFrom(src => src.Id.ToString())
                );

            CreateMap<UserBillToCreateDto, UserBill>();
        }
    }
}