using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using DataAccess;
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

            services
                .AddIdentityCore<User>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDBContext>();

            return services;
        }
    }
}
