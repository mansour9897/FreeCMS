namespace FreeCMS.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ControllerInfoAttribute:Attribute
    {
        private readonly string _name;
        private readonly string _areaName;

        public string Name { get => _name;}
        public string AreaName { get => _areaName;}

        public ControllerInfoAttribute(string name,string areaName)
        {
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException(string.Format("{0} is null or empty.",nameof(name)));
            
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentNullException(string.Format("{0} is null or empty.", nameof(areaName)));
            
            _name = name;
            _areaName = areaName;
        }
        
    }
    
}