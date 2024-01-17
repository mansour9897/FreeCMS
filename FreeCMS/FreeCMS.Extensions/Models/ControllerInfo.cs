namespace FreeCMS.Extensions.Models
{
    public class ControllerInfo
    {
        private List<PermissionInfo> _permissionList;
        public ControllerInfo()
        {
            this._permissionList = new List<PermissionInfo>();
        }
        public ControllerInfo(string name,string displayName)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this._permissionList = new List<PermissionInfo>();
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Area { get; set; }
        public string AreaName { get; set; }
        public string AssemblyName { get; set; }
        public List<PermissionInfo> Permissions { get => _permissionList;}

        public void AddPermission(string name,string displayName, string description)
        {
            PermissionInfo pi = new PermissionInfo(name,displayName,description);
            _permissionList.Add(pi);
        }
        
    }
    
}