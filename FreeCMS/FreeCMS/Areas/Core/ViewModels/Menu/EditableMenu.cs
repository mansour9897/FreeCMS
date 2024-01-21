namespace FreeCMS.Areas.Core.ViewModels.Menu
{
    public class EditableMenu
    {
        public EditableMenu(){}
        public EditableMenu(int id,string displayText,bool isPublic,string cssClass,string description)
        {
            this.Id = id;
            this.DisplayText = displayText;
            this.IsPublic = isPublic;
            this.CssClass = cssClass;
            this.Description = description;
        }
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public bool IsPublic { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public List<string> Parents { get; set; }
        public List<string> Text { get; set; }
        public List<string> Links { get; set; }
        public List<string> CssClasses { get; set; }
        public List<bool> OpenInNewTab { get; set; }
        public List<bool> LinkIsEnabled { get; set; }
    }
}