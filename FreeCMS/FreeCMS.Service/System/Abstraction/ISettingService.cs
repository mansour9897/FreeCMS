using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
    public interface ISettingService
    {
        List<Setting> GetAll();
        List<Setting> GetByType(string type);
        Setting AddSetting(Setting setting);
        Setting UpdateSetting(Setting setting);
        Setting GetByName(string name,string type);
        void RemoveSetting(Setting setting);
        //void RemoveByAssemblyName(string assemblyName);
        void RemoveByType(string type);
        bool SettingExist(string name,string type);

        
    }
}