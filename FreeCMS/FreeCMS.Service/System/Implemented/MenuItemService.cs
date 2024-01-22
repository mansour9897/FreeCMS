using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class MenuItemService:IMenuItemService
    {
        #region variables ...
        private readonly IMenuItemRepository _menuItemRepo;
        private readonly IMenuRepository _menuRepo;
        #endregion

        #region constructors ...
        public  MenuItemService(IMenuRepository menuTypeRepo,IMenuItemRepository menuItemRepo)
        {
            _menuRepo = menuTypeRepo;
            _menuItemRepo = menuItemRepo;
        }
        #endregion

        #region methods ...
        public List<MenuItem> GetAllItems(string menuType)
        {
            var menuTypeObj = _menuRepo.GetByName(menuType);
            if(menuTypeObj != null)
            {
                return _menuItemRepo.List(mi => mi.MenuId == menuTypeObj.Id) as List<MenuItem>;
            }
            return null;
        }
        public MenuItem Add(MenuItem item,string menuName)
        {
            var menuType = _menuRepo.GetByName(menuName);
            if(menuType != null)
            {
                item.MenuId = menuType.Id;
                item.CreationDate = DateTime.Now;
                return _menuItemRepo.Insert((MenuItem)item);
            }
            return null;
        }
        public MenuItem Add(MenuItem item)
        {
            item.CreationDate = DateTime.Now;
            return _menuItemRepo.Insert(item);
        }
        public bool ItemExist(string name,string menu)
        {
            var menuType = _menuRepo.GetByName(menu);
            if(menuType != null)
            {
                var item = _menuItemRepo.List(i => i.Name == name && i.MenuId == menuType.Id).FirstOrDefault();
                if(item != null)
                    return true;
            }
            return false;
        }
        public void Remove(string name, string menu)
        {
            var menuType = _menuRepo.GetByName(menu);
            if(menuType != null)
            {
                var item = _menuItemRepo.List(i => i.Name == name && i.MenuId == menuType.Id).FirstOrDefault();
                if(item != null)
                    _menuItemRepo.Delete(item);
            }
        }
        //public void RemoveByAssemblyName(string assemblyName)
        //{
        //    var items = _menuItemRepo.List(i => i.PluginName == assemblyName);
        //    foreach (var item in items)
        //    {
        //        _menuItemRepo.Delete(item);
        //    }
        //}
        public void RemoveByMenuId(int id)
        {
            var items = _menuItemRepo.List(i => i.MenuId == id);
            foreach (var item in items)
            {
                _menuItemRepo.Delete(item);
            }
        }
        #endregion
        
    }
    
}