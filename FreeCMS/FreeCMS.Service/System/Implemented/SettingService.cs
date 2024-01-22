using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class SettingService:ISettingService
    {
        #region variables ...
        private readonly ISettingRepository _settingRepo;
        #endregion

        #region constructors ...
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepo = settingRepository;
        }
        #endregion

        #region methods ...
        public List<Setting> GetAll()
        {
            return _settingRepo.List() as List<Setting>;
        }
        public List<Setting> GetByType(string type)
        {
            return _settingRepo.List(s => s.Type == type) as List<Setting>;
        }
        public Setting AddSetting(Setting setting)
        {
            return _settingRepo.Insert(setting);
        }
        public Setting UpdateSetting(Setting setting)
        {
            return _settingRepo.Update(setting);
        }
        public Setting GetByName(string name,string type)
        {
            return _settingRepo.List(s => s.Name == name && s.Type == type).FirstOrDefault();
        }
        public void RemoveSetting(Setting setting)
        {
            _settingRepo.Delete(setting);
        }
        //public void RemoveByAssemblyName(string assemblyName)
        //{
        //    var settings = _settingRepo.List(s => s.AssemblyName == assemblyName);
        //    foreach (var item in settings)
        //    {
        //        _settingRepo.Delete(item);
                
        //    }
        //}
        public void RemoveByType(string type)
        {
            var settings = GetByType(type);
            foreach(var item in settings)
            {
                _settingRepo.Delete(item);
            }
        }
        public bool SettingExist(string name,string type)
        {
            var setting = _settingRepo.List(s => s.Name == name && s.Type == type).FirstOrDefault();
            if(setting == null)
                return false;
            return true;
        }
        #endregion
    }
    
}