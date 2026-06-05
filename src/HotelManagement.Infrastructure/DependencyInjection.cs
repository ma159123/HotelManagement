using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Infrastructure.Middlewares;
using HotelManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<GlobalExceptionHandler>();

            services.AddTransient<IRoomsRepo, RoomsRepo>();
            services.AddTransient<IAmenityRepo, AmenityRepo>();

            return services;
        }
    }
}
