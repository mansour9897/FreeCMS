using System;
namespace FreeCMS.Attributes
{
    [AttributeUsage(AttributeTargets.Class,Inherited=false,AllowMultiple=false)]
    public class FreeCmsSelectListAttribute:Attribute
    {
        #region variables
        private string _title;
        private uint _priority;
        #endregion

        #region constructors 
        public FreeCmsSelectListAttribute(string title,uint priority = 0)
        {
            if(string.IsNullOrEmpty(title)) 
                throw new ArgumentNullException(string.Format("{0} is null or empty.",nameof(title)));
            this._title = title;
            this._priority = priority;
        }
        #endregion

        #region properties
        public string Title { get => _title;}
        public uint Priority { get => _priority;}
        
        #endregion


    }
}