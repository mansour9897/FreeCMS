using Microsoft.AspNetCore.Identity;

namespace FreeCMS.DomainModels.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public string? FullName => string.Format("{0} {1}", FirstName, LastName);

		public virtual ICollection<UserRole> UserRoles { get; set; }
		public bool Active { get; set; }
		public bool IsPermissionInUserRoles(string permission)
		{
			if (!this.Active || this.UserRoles == null)
				return false;
			foreach (var userRole in this.UserRoles)
			{
				var role = userRole.Role;
				if (role.IsPermissionInRole(permission))
				{
					return true;
				}
			}
			return false;
		}
		public bool IsAdmin()
		{
			if (this.UserRoles == null)
				return false;
			foreach (var userRole in this.UserRoles)
			{
				var role = userRole.Role;
				if (role.IsAdmin)
				{
					return true;
				}
			}
			return false;
		}

	}
}
