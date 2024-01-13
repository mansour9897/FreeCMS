using System.ComponentModel.DataAnnotations;
namespace FreeCMS.ViewModels.Profile
{
    public class ChangePasswordVm
    {
        [Required(ErrorMessage="وارد کردن رمز ورود الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} شما باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز ورود فعلی")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage="وارد کردن رمز ورود الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} شما باید حداقل {2} و حداکثر {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز ورود جدید")]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز ورود جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز ورود با تکرار آن یکسان نیست.")]
        public string ConfirmNewPassword { get; set; }
        
    }
    
}