using System;
namespace FreeCMS.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ActionInfoAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _description;
        public string Description { get => _description; }
        public string Name { get => _name; }

        public ActionInfoAttribute(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(string.Format("{0} is null or empty.", nameof(name)));
            this._name = name;
            this._description = description;

        }
    }

}