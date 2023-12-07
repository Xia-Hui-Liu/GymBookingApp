﻿using GymBookingApp.Data;
using GymBookingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace GymBookingApp.Data
{
    public class SeedData
    {
        private static ApplicationDbContext context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;


        public static async Task InitAsync(ApplicationDbContext _context, IServiceProvider services)
        {
            context = _context;

            if (context.Roles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "User", "Admin" };
            var adminEmail = "admin@admin.com";

            await AddRolesAsync(roleNames);

            var admin = await AddAccountAsync(adminEmail, "P@55w.rd");

            await AddUserToRoleAsync(admin, "Admin");

        }

        private static async Task AddUserToRoleAsync(ApplicationUser user, string role)
        {
            if (!await userManager.IsInRoleAsync(user, role))
            {
                var result = await userManager.AddToRoleAsync(user, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAccountAsync(string accountEmail, string password)
        {
            var found = await userManager.FindByEmailAsync(accountEmail);

            if (found != null) return null!;

            var user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user;

        }
    }
}