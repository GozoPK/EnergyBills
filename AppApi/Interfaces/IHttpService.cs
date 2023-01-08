using AppApi.DTOs;

namespace AppApi.Services
{
    public interface IHttpService
    {
        Task<TaxisnetUserDto> TaxisnetLogin(UserForLoginDto user);
    }
}