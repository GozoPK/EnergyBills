using System.Net;
using System.Text.Json;
using AppApi.Errors;

namespace AppApi.Middleware
{
    public class MiddlewareExceptions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExceptions> _logger;
        private readonly IHostEnvironment _env;
        public MiddlewareExceptions(RequestDelegate next, ILogger<MiddlewareExceptions> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() 
                    ? new ErrorException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ErrorException(context.Response.StatusCode, ex.Message, "Σφάλμα 500: Σφάλμα στον διακομιστή");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}