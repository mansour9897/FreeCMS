using Microsoft.AspNetCore.Identity;

namespace FreeCMS.DomainModels.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName => string.Format("{0} {1}", FirstName, LastName);
    }
}
