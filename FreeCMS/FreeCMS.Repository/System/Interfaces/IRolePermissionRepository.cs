using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Repository.System
{
    public interface IRolePermissionRepository
    {
        IList<RolePermission> GetAll();
        IList<RolePermission> GetByRoleId(string roleId);
    }
}
