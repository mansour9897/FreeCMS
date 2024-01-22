using FreeCMS.DomainModels.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class SelectListService:ISelectListService
    {
        #region variables
        private const string _selectListType = "FreeCmsSelectList";
        private readonly ISettingService _settingService;
        #endregion

        #region constructors
        public SelectListService(ISettingService settingService)
        {
            this._settingService = settingService;
        }
        #endregion

        #region methods
        public FreeCmsSelectList Add(FreeCmsSelectList selectList)
        {
            _settingService.AddSetting(new Setting(){
                Name = selectList.Name,
                Description = selectList.Title,
                Value = selectList.Priority.ToString(),
                Type = _selectListType
            });
            return selectList;
        }
        public FreeCmsSelectList Add(string title,string name,uint priority,string pluginName)
        {
			FreeCmsSelectList newSelectList = new FreeCmsSelectList();
            newSelectList.Name = name;
            //newSelectList.PluginName = pluginName;
            newSelectList.Title = title;
            newSelectList.Priority = priority;
            return Add(newSelectList);
        }
        public bool Exists(string name,string pluginName)
        {
            var item = _settingService.GetAll().Where(s => s.Name == name && s.Type == _selectListType ).FirstOrDefault();
            if(item == null) return false;
            return true;
        }
        public IList<FreeCmsSelectList> List()
        {
            var settings = _settingService.GetByType(_selectListType);
            return settings.Select(s => new FreeCmsSelectList(){
                Title = s.Description,
                Name = s.Name,
                Priority = uint.Parse(s.Value),
                //PluginName = s.AssemblyName
            }).ToList();
        }
        public void RemoveAll()
        {
            _settingService.RemoveByType(_selectListType);   
        }
        #endregion
    }
}