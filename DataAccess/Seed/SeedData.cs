using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Seed
{
    public class SeedData
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDBContext _context;

        public SeedData(RoleManager<Role> roleManager, ApplicationDBContext context)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedDatabase()
        {
            await _context.Database.MigrateAsync();
            await SeedRoles();
        }

        async Task SeedRoles()
        {
            if (await _roleManager.Roles.AnyAsync())
                return;

            foreach (var role in Enum.GetNames<RoleTypes>())
            {
                await _roleManager.CreateAsync(new Role { Name = role });
            }
        }
    }
}
