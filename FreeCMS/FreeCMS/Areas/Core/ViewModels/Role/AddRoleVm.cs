using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels
{
    public class AddRoleVm
    {
        [Required(ErrorMessage="وارد کردن نام الزامی است.")]
        [Display(Name="نام")]
        public string Name { get; set; }
        [Display(Name="توضیحات")]
        public string Description { get; set; }
        [Display(Name="مدیر است؟")]
        public bool IsAdmin { get; set; }
        
    }
    
}