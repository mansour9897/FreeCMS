using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Repository.Users
{
	public class UserRoleRepository: BaseRepository<UserRole, int>, IUserRolesRepository
	{
		public UserRoleRepository(FreeCMSContext context)
			:base(context) { }
	}
}
