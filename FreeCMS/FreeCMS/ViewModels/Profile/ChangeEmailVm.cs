using System.ComponentModel.DataAnnotations;
namespace FreeCMS.ViewModels.Profile
{
    public class ChangeEmailVm
    {
        [Required(ErrorMessage="وارد کردن ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage="ایمیل وارد شده معتبر نمی باشد.")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        
    }
    
}