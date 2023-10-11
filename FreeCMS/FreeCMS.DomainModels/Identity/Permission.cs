using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.DomainModels.Identity
{
	public class Permission
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public string AssemblyName { get; set; }
		public string AreaName { get; set; }
		public string AreaNameShow { get; set; }
		public string ControllerName { get; set; }
		public string ControllerNameShow { get; set; }
		public string ActionName { get; set; }
		public string ActionNameShow { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
		public string PermissionRoute
		{
			get
			{
				return AreaName + "-" + ControllerName + "-" + ActionName;
			}
		}
		public string FullDisplayName
		{
			get
			{
				return AreaNameShow + "-" + ControllerNameShow + "-" + ActionNameShow;
			}
		}
	}
}
