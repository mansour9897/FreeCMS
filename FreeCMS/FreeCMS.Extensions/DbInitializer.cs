using FreeCMS.DomainModels.Identity;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Extensions.Models;
using FreeCMS.Service.System;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Web.Mvc;

namespace FreeCMS.Extensions
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void InitializeAsync()
        {
            string _roleName = "مدیر ارشد";
            string userEmail = "admin@freecms.com";
            string password = "Fr33cm5!!";

            await AddAdminRoleAsync(_roleName);
            await AddAdminUserAsync(userEmail, password, _roleName);

           
        }

        private async Task AddAdminRoleAsync(string roleName)
        {

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role()
                { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task AddAdminUserAsync(string email, string pass, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    LastName = role
                };
                var result = _userManager.CreateAsync(user, pass).Result;

            }
            if (user != null)
            {
                if (!_userManager.IsInRoleAsync(user, role).Result)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }

        
        
    }
}

