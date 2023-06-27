using System.Text.Json;
using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Seed
{
    public class SeedData
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _context;

        public SeedData(
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            ApplicationDBContext context
        )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedDatabase()
        {
            await _context.Database.MigrateAsync();
            await SeedRoles();
            await SeedProducts();
            await SeedUsers();
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

        async Task SeedProducts()
        {
            if (await _context.Products.AnyAsync())
                return;

            var data = await File.ReadAllTextAsync("../DataAccess/Seed/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(data);

            if (products != null)
            {
                foreach (var product in products)
                {
                    await _context.Products.AddAsync(product);
                }
                await _context.SaveChangesAsync();
            }
        }

        async Task SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "Badr",
                    Name = "Badr",
                    Email = "badr@test.com",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1998, 01, 31),
                    LastActive = DateTime.UtcNow
                };

                var s = await _userManager.CreateAsync(user, "Pa$$w0rd");
                await _userManager.AddToRoleAsync(user, RoleTypes.User.ToString());

                var admin = new User
                {
                    UserName = "admin",
                    Name = "admin",
                    Email = "admin@test.com",
                    Gender = "Unknown",
                    DateOfBirth = new DateTime(1998, 01, 31),
                    LastActive = DateTime.UtcNow
                };

                await _userManager.CreateAsync(admin, "Pa$$w0rd");
                await _userManager.AddToRolesAsync(admin, Enum.GetNames<RoleTypes>());
            }
        }
    }
}
