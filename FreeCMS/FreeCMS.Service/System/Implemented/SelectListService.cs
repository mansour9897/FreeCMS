using FreeCMS.DomainModels.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class SelectListService:ISelectListService
    {
        #region variables
        private const string _selectListType = "WeboSelectList";
        private readonly ISettingService _settingService;
        #endregion

        #region constructors
        public SelectListService(ISettingService settingService)
        {
            this._settingService = settingService;
        }
        #endregion

        #region methods
        public WeboSelectList Add(WeboSelectList selectList)
        {
            _settingService.AddSetting(new Setting(){
                Name = selectList.Name,
                AssemblyName = selectList.PluginName,
                Description = selectList.Title,
                Value = selectList.Priority.ToString(),
                Type = _selectListType
            });
            return selectList;
        }
        public WeboSelectList Add(string title,string name,uint priority,string pluginName)
        {
            WeboSelectList newSelectList = new WeboSelectList();
            newSelectList.Name = name;
            newSelectList.PluginName = pluginName;
            newSelectList.Title = title;
            newSelectList.Priority = priority;
            return Add(newSelectList);
        }
        public bool Exists(string name,string pluginName)
        {
            var item = _settingService.GetAll().Where(s => s.Name == name && s.AssemblyName == pluginName
                && s.Type == _selectListType ).FirstOrDefault();
            if(item == null) return false;
            return true;
        }
        public IList<WeboSelectList> List()
        {
            var settings = _settingService.GetByType(_selectListType);
            return settings.Select(s => new WeboSelectList(){
                Title = s.Description,
                Name = s.Name,
                Priority = uint.Parse(s.Value),
                PluginName = s.AssemblyName
            }).ToList();
        }
        public void RemoveAll()
        {
            _settingService.RemoveByType(_selectListType);   
        }
        #endregion
    }
}