using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
    {
        //Registering DataBase using Entities Framework Service
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        //Registering Angular app 
        services.AddCors();

        //Registering TokenService for JWT 
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
