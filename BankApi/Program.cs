using BankApi.Data;
using BankApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextPool<BankContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDbConnection"),
        new MariaDbServerVersion(new Version(10,6,11))));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
