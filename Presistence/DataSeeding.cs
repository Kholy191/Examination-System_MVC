using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Presistence
{
    public class DataSeeding : IDataSeeding
    {
        readonly ApplicationDbContext _context;
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        public DataSeeding(ApplicationDbContext dbcontext, UserManager<ApplicationUser> _usermanager, RoleManager<IdentityRole> _rolemanager)
        {
            _context = dbcontext;
            _userManager = _usermanager;
            _roleManager = _rolemanager;
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!await _context.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("Student"));
                    await _roleManager.CreateAsync(new IdentityRole("Instructor"));
                }
                if (!await _context.Users.AnyAsync())
                {
                    var User1 = new ApplicationUser()
                    {
                        UserName = "Kholy123",
                        Email = "a.elkholy2711@gmail.com",
                    };

                    var Result = await _userManager.CreateAsync(User1, "Kholy@1911");
                    if (!Result.Succeeded)
                    {
                        throw new Exception("Failed to create users: " + string.Join(", ", Result.Errors.Select(e => e.Description)));
                    }
                    var user1 = await _userManager.FindByEmailAsync(User1.Email);
                    if (user1 == null)
                    {
                        throw new Exception("Failed to find created users by email.");
                    }
                    Result = await _userManager.AddToRoleAsync(user1, "Admin");
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Notify the error to the user or log it
                throw new Exception("An error occurred while seeding identity data: " + ex.Message, ex);
            }
        }
    }
}
