using HotelManagement.Application.Interfaces.Repositories;
using HotelManagement.Application.Interfaces.Services;
using HotelManagement.Application.Settings;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Data;
using HotelManagement.Infrastructure.Middlewares;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookingServices = HotelManagement.Infrastructure.Services.BookingServices;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBookingService, BookingServices>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.Configure<EmailSettings>(
    configuration.GetSection("EmailSettings"));

            services.AddScoped<IEmailService, EmailService>();
            // services.AddScoped<ITokenCache, RedisTokenCache>();
            services.AddScoped<ITokenCache, MemoryTokenCache>();
            // Identity
            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                // Password
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // Email
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                // Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = configuration.GetConnectionString("Redis");
            //    options.InstanceName = "HotelManagement_";
            //});
            services.AddMemoryCache();
            return services;
        }
    }
}
