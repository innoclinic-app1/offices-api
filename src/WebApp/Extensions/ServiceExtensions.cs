using System.Text.Json;
using Domain.Dtos;
using Domain.Profiles;
using Domain.Validators;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace WebApp.Extensions;

internal static class ServiceExtensions
{
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        
        services.AddEndpointsApiExplorer();
        
        return services;
    }
    
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOfficeRepository, OfficeRepository>();
        
        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IOfficeService, OfficeService>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OfficeProfile));
        
        return services;
    }
    
    public static IServiceCollection ConfigureValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<OfficeValidator<OfficeDto>>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddFluentValidationRulesToSwagger();
        services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Office API" }));
        
        return services;
    }

    public static IServiceCollection ConfigureDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<DataContext>(options => options.UseSqlServer(connectionString));
        
        return services;
    }
}
