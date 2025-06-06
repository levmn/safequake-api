using Microsoft.EntityFrameworkCore;
using SafeQuake.Application.Interfaces.Data;
using SafeQuake.Application.Interfaces.Earthquake;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Application.UseCases.Earthquake;
using SafeQuake.Application.UseCases.User;
using SafeQuake.Infrastructure.Persistence;
using SafeQuake.Service.Interfaces;
using SafeQuake.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SafeQuake API", 
        Version = "v1",
        Description = "API para monitoramento de eventos sísmicos e alertas de terremotos"
    });
});

try
{
    // Database
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseOracle(DatabaseConfiguration.GetConnectionString()));

    // Register DbContext
    builder.Services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);
}
catch (Exception ex)
{
    Console.WriteLine($"Error configuring database: {ex.Message}");
    throw; // Re-throw to prevent the application from starting with invalid configuration
}

// Register Use Cases
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

builder.Services.AddScoped<ICreateEarthquakeUseCase, CreateEarthquakeUseCase>();
builder.Services.AddScoped<IGetEarthquakeUseCase, GetEarthquakeUseCase>();
builder.Services.AddScoped<IUpdateEarthquakeUseCase, UpdateEarthquakeUseCase>();

// Register Services
builder.Services.AddHttpClient<IEarthquakeService, EarthquakeService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable HTTPS redirection in development
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
    throw;
}
