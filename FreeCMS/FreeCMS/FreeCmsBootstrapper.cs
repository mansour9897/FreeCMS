using FreeCMS.DomainModels.Identity;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Extensions.Models;
using FreeCMS.Service.System;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FreeCMS
{
    public static class FreeCmsBootstrapper
    {
        public static void AddPermissions(IPermissionService permissionService)
        {
            var currentPermissions = GetAllPermissions();
            //go deep to permissions and add each one of them
            foreach (var controller in currentPermissions)
            {
                foreach (var permission in controller.Permissions)
                {
                    Permission per = new Permission();
                    //per.Name = permission.Name;
                    per.Name = permission.DisplayName;
                    //per.AssemblyName = currentPermissions.AssemblyName;

                    //per.AreaName = currentPermissions.AreaName;

                    per.ControllerName = controller.Name;
                    per.ControllerNameShow = controller.DisplayName;

                    per.ActionName = permission.Name;
                    per.ActionNameShow = permission.DisplayName;
                    per.Description = permission.Description;
                    per.AreaName = controller.Area;
                    per.AreaNameShow = controller.AreaName;
                    per.AssemblyName = controller.AssemblyName;
                    if (!permissionService.PermissionExist(per))
                    {
                        permissionService.AddPermission(per);
                    }
                }
            }
        }
        public static List<ControllerInfo> GetAllPermissions()
        {
            List<ControllerInfo> controllerList = new List<ControllerInfo>();
            Assembly asm = Assembly.GetExecutingAssembly();
            IEnumerable<Type> controlers = asm.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).OrderBy(x => x.Name);
            foreach (Type cls in controlers)
            {
                var customController = cls.GetCustomAttribute<ControllerInfoAttribute>();
                if (customController != null)
                {
                    ControllerInfo controllerVM = new ControllerInfo();
                    controllerVM.Name = cls.Name;
                    controllerVM.DisplayName = customController.Name;
                    controllerVM.Area = cls.GetCustomAttribute<AreaAttribute>().RouteValue;
                    controllerVM.AssemblyName = cls.GetCustomAttribute<AreaAttribute>().RouteValue;
                    controllerVM.AreaName = customController.AreaName;
                    IEnumerable<MemberInfo> memberInfo = cls.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
                    foreach (MemberInfo method in memberInfo)
                    {
                        var customMethod = method.GetCustomAttribute<ActionInfoAttribute>();
                        if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && (customMethod != null))
                        {
                            string actionName = method.Name;
                            string nameShow = customMethod.Name;
                            string description = customMethod.Description;
                            controllerVM.AddPermission(actionName, nameShow, description);
                        }
                    }
                    controllerList.Add(controllerVM);
                }
            }

            return controllerList;
        }

        
    }
}
