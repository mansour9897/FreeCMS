using FreeCMS.DomainModels.Identity;
using FreeCMS.DomainModels.System;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace FreeCMS.Extensions
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMenuService _menuService;
        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, IMenuService menuService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _menuService = menuService;
        }

        public async void InitializeAsync()
        {
            string _roleName = "مدیر ارشد";
            string userEmail = "admin@freecms.com";
            string password = "Fr33cm5!!";

            await AddAdminRoleAsync(_roleName);
            await AddAdminUserAsync(userEmail, password, _roleName);

            if (_menuService.MenuTypeExist(MainMenuNames.PublicMainMenu) == false)
            {
                Menu menu = new Menu()
                {
                    Name = MainMenuNames.PublicMainMenu,
                    IsPrivate = false,
                    CssClass = "webo-public-main-menu",
                    DisplayText = "منو اصلی سایت",
                    Direction = MenuDirection.Horizontal,
                    Description = "منوی اصلی بالای وبسایت"
                };
                _menuService.Add(menu);
            }


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

