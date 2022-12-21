using AutoMapper;
using TaxisNetSimApi.DTOs;
using TaxisNetSimApi.Entities;

namespace TaxisNetSimApi.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TaxisNetUserEntity, UserToReturnDto>();
        }
    }
}