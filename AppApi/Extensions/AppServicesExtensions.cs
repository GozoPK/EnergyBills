using AppApi.Data;
using AppApi.Services;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<DataContext>(options => 
                options.UseMySql(config.GetConnectionString("MariaDbConnection"),
                    new MariaDbServerVersion(new Version(10,6,11))));

            services.AddCors();
            services.AddHttpClient();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHttpService, HttpService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}