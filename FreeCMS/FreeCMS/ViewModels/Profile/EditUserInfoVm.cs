using FreeCMS.DomainModels.Identity;
using System.ComponentModel.DataAnnotations;
namespace FreeCMS.ViewModels.Profile
{
    public class EditUserInfoVm
    {
        public EditUserInfoVm(){}
        public EditUserInfoVm(ApplicationUser user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }
        [Display(Name="نام")]
        public string FirstName { get; set; }
        [Display(Name="نام خانوادگی")]
        public string LastName { get; set; }
        
    }
    
}