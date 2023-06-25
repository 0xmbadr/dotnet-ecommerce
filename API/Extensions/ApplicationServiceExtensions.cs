using API.Services;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddDbContext<ApplicationDBContext>(
                opts => opts.UseSqlServer(config.GetConnectionString("DBConnection"))
            );

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();

            services
                .AddIdentityCore<User>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddSignInManager<SignInManager<User>>();

            services.AddAuthentication();

            return services;
        }
    }
}
