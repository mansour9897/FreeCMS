namespace FreeCMS.Extensions.Models
{
    public class PermissionInfo
    {
        public PermissionInfo(){}
        public PermissionInfo(string name,string displayName,string description)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Description = description;
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        
    }
    
}