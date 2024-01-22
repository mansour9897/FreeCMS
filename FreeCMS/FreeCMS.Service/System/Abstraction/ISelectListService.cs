namespace FreeCMS.Service.System.Abstraction
{
    public interface ISelectListService
    {
		FreeCmsSelectList Add(FreeCmsSelectList selectList);
		FreeCmsSelectList Add(string title,string name,uint priority,string pluginName);
        bool Exists(string name,string pluginName);
        IList<FreeCmsSelectList> List();
        void RemoveAll();
    }
    public class FreeCmsSelectList
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public uint Priority { get; set; }
    }
}