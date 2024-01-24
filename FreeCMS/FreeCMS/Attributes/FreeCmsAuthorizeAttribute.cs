using FreeCMS.Service.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Attributes
{
    public class FreeCmsAuthorizeAttribute:TypeFilterAttribute
    {    
        public FreeCmsAuthorizeAttribute()
            :base(typeof(FreeCmsPermissionFilter))
        {}
    }
    
}