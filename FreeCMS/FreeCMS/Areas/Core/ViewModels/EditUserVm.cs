using FreeCMS.DomainModels.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels
{
    public class EditUserVm
    {
        public EditUserVm(){}
        public EditUserVm(ApplicationUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Active = user.Active;
            this.UserRoles = user.UserRoles;
        }
        [Display(Name="کد")]
        public string Id { get; set; }
        [Display(Name="نام")]
        public string FirstName { get; set; }
        [Display(Name="نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name="فعال")]
        public bool Active { get; set; }
        [Display(Name="تصویر")]
        public byte[] Image { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
    }
    
}