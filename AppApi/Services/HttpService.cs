using System.Text;
using System.Text.Json;
using AppApi.DTOs;

namespace AppApi.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public HttpService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;            
        }

        public async Task<TaxisnetUserDto> TaxisnetLogin(UserForLoginDto user)
        {
            var url  = _config["TaxisnetUrl"];

            var requestBody = JsonSerializer.Serialize(user);
            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{url}/account/login", requestContent);

            if ((int)response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<TaxisnetUserDto>(content, option);
        }
    }
}