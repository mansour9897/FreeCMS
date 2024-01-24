using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System
{
    public class MenuService:IMenuService
    {
        #region variables ...
        private readonly IMenuRepository _menuRepo;
        #endregion

        #region constructors ...
        public MenuService(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }
        #endregion

        #region methods ...
        public List<Menu> GetAllMenuTypes()
        {
            return _menuRepo.List() as List<Menu>;
        }
        public Menu Add(Menu menu)
        {
            menu.CreationDate = DateTime.Now;
            return _menuRepo.Insert(menu);
        }
        public Menu Add(Menu menu,string pluginName)
        {
            menu.CreationDate = DateTime.Now;
            return Add(menu);
        }
        public Menu Update(Menu menu)
        {
            return _menuRepo.Update(menu);
        }
        public Menu GetMenuTypeById(int id)
        {
            return _menuRepo.Get(id);
        }
        public Menu GetMenuTypeByName(string name)
        {
            //return _menuRepo.List(mt => mt.Name == name).FirstOrDefault();
            return _menuRepo.GetByName(name);
        }
        public bool MenuTypeExist(string name)
        {
            var mt = _menuRepo.List().Where(mt => mt.Name == name).FirstOrDefault();
            if(mt == null)
            {
                return false;
            }
            return true;
        }
        public bool MenuTypeExist(Menu menu)
        {
            return this.MenuTypeExist(menu.Name);
        }
        public void RemoveMenuType(int id)
        {
            var mt = _menuRepo.Get(id);
            if(mt != null)
            {
                _menuRepo.Delete(mt);
            }
        }
        #endregion
    }
}