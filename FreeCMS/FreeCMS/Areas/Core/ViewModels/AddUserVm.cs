using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels
{
    public class AddUserVm
    {
        [Required(ErrorMessage="وارد کردن نام کاربری الزامی است.")]
        [Display(Name="نام کاربری")]
        public string? UserName { get; set; }
        

        [EmailAddress(ErrorMessage="ایمیل وارد شده معتبر نمی باشد.")]
        [Required(ErrorMessage="وارد کردن ایمیل الزامی است.")]
        [Display(Name="ایمیل")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage="شماره موبایل وارد شده قالب صحیحی ندارد.")]
        [Required(ErrorMessage="وارد کردن شماره موبایل الزامی است.")]
        [Display(Name="شماره موبایل")]
        public string? MobileNumber { get; set; }
        [Display(Name="نام")]
        public string? FirstName { get; set; }
        [Display(Name="نام خانوادگی")]
        public string? LastName { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} شما باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage="وارد کردن رمز ورود الزامی است.")]
        [Display(Name="رمز ورود")]
        public string? Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="تکرار رمز با رمز یکسان نیست.")]
        [Display(Name="تکرار رمز ورود")]
        public string? ConfirmPassword { get; set; }
    }
    
}