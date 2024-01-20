using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IRolePermissionServices
    {
        IList<RolePermission> GetByRolesId(string roleId);
    }
}
