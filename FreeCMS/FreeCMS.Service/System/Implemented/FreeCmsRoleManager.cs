using FreeCMS.DomainModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FreeCMS.Service.System.Implemented
{
    public class FreeCmsRoleManager : RoleManager<Role>
    {
        public FreeCmsRoleManager(IRoleStore<Role> store,
                                  IEnumerable<IRoleValidator<Role>> roleValidators, 
                                  ILookupNormalizer keyNormalizer, 
                                  IdentityErrorDescriber errors, 
                                  ILogger<RoleManager<Role>> logger) 
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }


        public bool AddPermissionToRole(string roleId, int permissionId)
        {
            bool result = false;
            try
            {
                var role = this.FindByIdAsync(roleId.ToString()).Result;
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
        public bool RemovePermissionFromRole(string roleId, int permissionId)
        {
            bool result = false;
            try
            {
                var role = this.FindByIdAsync(roleId.ToString()).Result;
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
        public void AddPermissionsToRole(string roleId, List<int> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                this.AddPermissionToRole(roleId, permissionId);

            }
        }
    }
}
