using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AppApi.DTOs;
using AppApi.Errors;

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

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new StatusCodeException(new ErrorResponse((int)response.StatusCode));
            }

            var content = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<TaxisnetUserDto>(content, option);
        }

        public async Task<TaxisnetUserDto> GetUser(string token)
        {
            var url  = _config["TaxisnetUrl"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync($"{url}/account");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new StatusCodeException(new ErrorResponse((int)response.StatusCode));
            }

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<TaxisnetUserDto>(content, options);
        }
    }
}