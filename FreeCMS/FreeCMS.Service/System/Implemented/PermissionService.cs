using FreeCMS.DomainModels.Identity;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System
{
    public class PermissionService:IPermissionService
    {
        #region variables ...
        private readonly IPermissionRepository _permRepo;
        #endregion

        #region constructors ...
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permRepo = permissionRepository;
        }
        #endregion

        #region methods ...
        public Permission AddPermission(Permission permission)
        {
            return _permRepo.Insert(permission);
        }
        public Permission GetPermissionById(int id)
        {
            return _permRepo.Get(id);
        }
        public bool PermissionExist(Permission permission)
        {
            return this.PermissionExist(permission.AssemblyName, permission.AreaName, permission.ControllerName, permission.ActionName);
        }
        public bool PermissionExist(string assemblyName, string areaName, string controllerName, string actionName)
        {
            Permission per = _permRepo.List().Where(p => p.AssemblyName == assemblyName && p.AreaName == areaName && p.ControllerName == controllerName &&
                p.ActionName == actionName).FirstOrDefault();
            if (per == null)
                return false;
            return true;
        }
        public void RemovePermission(int id)
        {
            Permission perm = _permRepo.Get(id);
            if(perm != null)
            {
                _permRepo.Delete(perm);
            }
        }
        public void RemovePermissionsByAssembleyName(string assemblyName)
        {
            //var permissions = _permRepo.List(p => p.AssemblyName == assemblyName);
            //foreach (var per in permissions)
            //{
            //    this.RemovePermission(per.Id);
            //}
        }
        public List<Permission> GetPermissions()
        {
            return _permRepo.List() as List<Permission>;
        }
        public Permission UpdatePermission(Permission permission)
        {
            return _permRepo.Update(permission);
        }
        
        #endregion
    }
    
}