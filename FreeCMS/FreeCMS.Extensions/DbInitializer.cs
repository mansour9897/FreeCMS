using Microsoft.AspNetCore.Identity;

namespace FreeCMS.Extensions
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void InitializeAsync()
        {
            string roleName = "مدیر ارشد";
            if(! await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole()
                { Name = roleName };
                await _roleManager.CreateAsync(role);
            }

            string userEmail = "admin@freecms.com";
            string password = "fr33cm5!!";
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = userEmail,
                    Email = userEmail,
                     EmailConfirmed= true,
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
