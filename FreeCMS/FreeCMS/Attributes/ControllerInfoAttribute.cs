using System;
namespace FreeCMS.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ControllerInfoAttribute:Attribute
    {
        private readonly string _name;

        public string Name { get => _name;}

        public ControllerInfoAttribute(string name)
        {
            if(string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException(string.Format("{0} is null or empty.",nameof(name)));
            this._name = name;
        }
        
    }
    
}