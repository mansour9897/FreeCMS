using FreeCMS.DomainModels.Identity;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreeCMS.Service.System.Implemented
{
    public class FreeCmsRoleManager : RoleManager<Role>
    {
        private readonly IRolePermissionServices _rolePermissionServices;
        public FreeCmsRoleManager(IRoleStore<Role> store,
                                  IEnumerable<IRoleValidator<Role>> roleValidators, 
                                  ILookupNormalizer keyNormalizer, 
                                  IdentityErrorDescriber errors, 
                                  ILogger<RoleManager<Role>> logger,
                                  IRolePermissionServices rolePermissionServices) 
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _rolePermissionServices = rolePermissionServices;
        }


        public bool AddPermissionToRole(string roleId, int permissionId)
        {
            bool result = false;
            try
            {
                var role = this.FindByIdAsync(roleId.ToString()).Result;
                role.RolePermissions = _rolePermissionServices.GetByRolesId(roleId);
                if (role != null)
                {
                    var rolePermission = role.RolePermissions.Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                        .FirstOrDefault();
                    if (rolePermission == null)
                    {
                        result = true;
                        role.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
                        var updateResult = this.UpdateAsync(role).Result;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return result;
        }
        public void AddPermissionsToRole(string roleId, List<int> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                this.AddPermissionToRole(roleId, permissionId);

            }
        }

        public bool RemovePermissionFromRole(string roleId, int permissionId)
        {
            bool result = false;
            try
            {
                var role = this.FindByIdAsync(roleId.ToString()).Result;
                role.RolePermissions = _rolePermissionServices.GetByRolesId(roleId);
                if (role != null)
                {
                    var rolePermission = role.RolePermissions.Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                        .FirstOrDefault();
                    if (rolePermission != null)
                    {
                        result = true;
                        role.RolePermissions.Remove(rolePermission);
                        var updateResult = this.UpdateAsync(role).Result;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return result;
        }
    }
}
