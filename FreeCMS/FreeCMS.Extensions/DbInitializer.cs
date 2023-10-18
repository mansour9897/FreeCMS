using FreeCMS.DomainModels.Identity;
using Microsoft.AspNetCore.Identity;

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
            string roleName = "مدیر ارشد";
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role()
                { Name = roleName };
                await _roleManager.CreateAsync(role);
            }

            string userEmail = "admin@freecms.com";
            string password = "Fr33cm5!!";
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed= true,
                    LastName = roleName
                };
                var result = _userManager.CreateAsync(user, password).Result;
                
            }
            if (user != null)
            {
                if (!_userManager.IsInRoleAsync(user, roleName).Result)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
