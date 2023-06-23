using Core.Entities;
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
