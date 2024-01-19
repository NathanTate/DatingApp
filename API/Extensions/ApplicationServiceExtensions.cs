using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
    {
        //Registering Angular app 
        services.AddCors();

        //Registering TokenService for JWT 
        services.AddScoped<ITokenService, TokenService>();
        //Adding UserRepository, so we don't have to use DbContext all over out app (watch lesson 92 for pros and cons)
        //Adding Automapper from nuget for mapping our entity to dto
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
