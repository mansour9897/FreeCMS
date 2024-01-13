using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreeCMS.DomainModels.Identity
{
	public class Role : IdentityRole
	{
		public Role() { }
		public Role(string name)
		{
			Name = name;
		}
		public string? Description { get; set; }
		public bool IsAdmin { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
		#region method ...
		public bool IsPermissionInRole(string permission)
		{
			foreach (var rp in RolePermissions)
			{
				if (rp.Permission.PermissionRoute == permission)
				{
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
