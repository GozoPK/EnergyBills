using AppApi.DTOs;
using AppApi.Entities;

namespace AppApi.Services
{
    public interface ITokenService
    {
        string CreateToken(AccountToReturnDto account);
        string CreateToken(TaxisnetUserDto user);
    }
}