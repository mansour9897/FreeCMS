namespace FreeCMS.Service.System.Abstraction
{
    public interface ISelectListService
    {
        WeboSelectList Add(WeboSelectList selectList);
        WeboSelectList Add(string title,string name,uint priority,string pluginName);
        bool Exists(string name,string pluginName);
        IList<WeboSelectList> List();
        void RemoveAll();
    }
    public class WeboSelectList
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public uint Priority { get; set; }
        public string PluginName { get; set; }
    }
}