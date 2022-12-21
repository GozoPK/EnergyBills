using AutoMapper;
using BankApi.DTOs;
using BankApi.Entities;
using BankApi.Extensions;

namespace BankApi.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(
                    destination => destination.Name,
                    options => options.MapFrom(src => $"{src.LastName} {src.FirstName}"));

            CreateMap<TransactionForCreationDto, Transaction>();                
            CreateMap<Transaction, TransactionToReturnDto>()
            .ForMember(
                    destination => destination.TransactionType,
                    options => options.MapFrom(src => src.TransactionType.GetTransactionType()));
        }
    }
}