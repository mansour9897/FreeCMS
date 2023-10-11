using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.DomainModels.Identity
{
	public class UserRole : IdentityUserRole<int>
	{
		#region properties ...
		public virtual ApplicationUser User { get; set; }
		public virtual Role Role { get; set; }
		public bool IsAdmin
		{
			get
			{
				return this.Role.IsAdmin;
			}
		}
		#endregion

		#region methods ...
		public bool IsPermissionInRole(string permisson)
		{
			return this.Role.IsPermissionInRole(permisson);
		}
		#endregion
	}
}
