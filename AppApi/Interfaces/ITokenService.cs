using AppApi.DTOs;
using AppApi.Entities;

namespace AppApi.Services
{
    public interface ITokenService
    {
        string CreateToken(UserToReturnDto account);
        string CreateToken(TaxisnetUserDto user);
    }
}