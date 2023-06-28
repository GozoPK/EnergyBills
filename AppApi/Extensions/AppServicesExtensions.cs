using AppApi.Data;
using AppApi.Errors;
using AppApi.Interfaces;
using AppApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(entry => entry.Value.Errors.Count > 0)
                        .SelectMany(entry => entry.Value.Errors)
                        .Select(error => error.ErrorMessage);

                    var response = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // MariaDB Connection Configuration
            services.AddDbContextPool<DataContext>(options => 
                options.UseMySql(config.GetConnectionString("MariaDbConnection"),
                    new MariaDbServerVersion(new Version(10,11,2))));

            services.AddCors();
            services.AddHttpClient();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserBillsRepository, UserBillsRepository>();
            services.AddScoped<IBillsRepository, BillsRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHttpService, HttpService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}