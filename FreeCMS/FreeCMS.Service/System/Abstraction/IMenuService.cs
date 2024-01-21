using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IMenuService
    {
        List<Menu> GetAllMenuTypes();
        Menu Add(Menu menu);
        Menu Add(Menu menu,string pluginName);
        Menu Update(Menu menu);
        Menu GetMenuTypeById(int id);
        Menu GetMenuTypeByName(string name);
        bool MenuTypeExist(string name);
        bool MenuTypeExist(Menu menu);
        void RemoveMenuType(int id);
    }
    
}