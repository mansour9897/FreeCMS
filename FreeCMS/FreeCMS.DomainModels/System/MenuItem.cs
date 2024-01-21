namespace FreeCMS.DomainModels.System
{
    public enum MenuDirection
    {
        Horizontal,
        Vertical
    }

    public static class MainMenuNames
    {
        public static string ManagementMainMenu =>  "ManagementMainMenu";
        public static string ManagementMainHeaderMenu =>  "ManagementMainHeaderMenu";
        public static string PublicMainMenu => "PublicMainMenu";
        public static string PanelMainMenu => "PanelMainMenu";
    }

    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPrivate { get; set; }
        public string CssClass { get; set; }
        public string Description { get; set; }
        public string DisplayText { get; set; }
        public MenuDirection Direction { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set;}
    }
    
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string PluginName { get; set; }
        public string Url { get; set; }
        public string ManagePermission { get; set; }
        public int ShowPriority { get; set; }
        public bool OpenInNewWindow { get; set; }
        public string CssClass { get; set; }
        public bool LinkIsEditable { get; set; }
        public int MenuId { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual MenuItem Parent { get; set; }
        public virtual ICollection<MenuItem> Children { get; set; }
    }
    
}