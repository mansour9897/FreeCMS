using FreeCMS.DomainModels.Identity;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;
using System.Security;

namespace FreeCMS.Service.System.Implemented
{
    public class RolePermissionService : IRolePermissionServices
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IRolePermissionRepository repository)
        {
            _rolePermissionRepository = repository;
        }
        public IList<RolePermission> GetByRolesId(string roleId)
        {
            return _rolePermissionRepository.GetByRoleId(roleId);

        }
    }
}
