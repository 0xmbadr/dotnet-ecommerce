using System.Text.Json;
using AutoMapper;
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
        private readonly IMapper __mapper;

        public SeedData(
            RoleManager<Role> roleManager,
            ApplicationDBContext context,
            IMapper _mapper
        )
        {
            __mapper = _mapper;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task SeedDatabase()
        {
            await _context.Database.MigrateAsync();
            await SeedRoles();
            await SeedProducts();
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
    }
}
