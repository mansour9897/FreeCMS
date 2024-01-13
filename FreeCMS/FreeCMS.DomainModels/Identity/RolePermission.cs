using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreeCMS.DomainModels.Identity
{
	public class RolePermission
	{
		[Column(Order = 0), Key]
		public string RoleId { get; set; }
		public virtual Role Role { get; set; }
		[Column(Order = 1), Key]
		public int PermissionId { get; set; }
		public virtual Permission Permission { get; set; }

	}
}
