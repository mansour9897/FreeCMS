using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IPermissionService
    {
        Permission AddPermission(Permission permission);
        Permission GetPermissionById(int id);
        bool PermissionExist(Permission permission);
        bool PermissionExist(string assembleName,string areaName,string controllerName,string actionName);
        Permission UpdatePermission(Permission permission);
        void RemovePermission(int id);
        void RemovePermissionsByAssembleyName(string assemblyName);
        List<Permission> GetPermissions();
    }
    
}