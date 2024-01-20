using FreeCMS.DomainModels.Identity;
using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels
{
    public class UserDetails
    {
        public UserDetails(){}
        public UserDetails(ApplicationUser user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            //this.RegisterDate = user.RegisterDate;
            this.Active = user.Active;
            this.UserRoles = user.UserRoles;
        }
        [Display(Name="کد")]
        public string Id { get; set; }
        [Display(Name="نام کاربری")]
        public string UserName { get; set; }
        [Display(Name="ایمیل")]
        public string Email { get; set; }

        [Display(Name="شماره موبایل")]
        public string MobileNumber { get; set; }
        [Display(Name="نام")]
        public string FirstName { get; set; }
        [Display(Name="نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name="تصویر")]
        public byte[] Image { get; set; }
        [Display(Name="تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        [Display(Name="فعال")]
        public bool Active { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [Display(Name="نام")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
    
}