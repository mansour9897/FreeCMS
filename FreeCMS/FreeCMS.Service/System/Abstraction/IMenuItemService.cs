using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllItems(string menuType);
        MenuItem Add(MenuItem item, string menuName);
        MenuItem Add(MenuItem item);
        bool ItemExist(string name, string menu);
        void Remove(string name, string menu);
        //void RemoveByAssemblyName(string assemblyName);
        void RemoveByMenuId(int id);
    }

}