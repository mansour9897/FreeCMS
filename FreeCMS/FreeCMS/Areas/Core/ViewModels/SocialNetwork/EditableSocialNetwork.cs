using System;
using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels
{
    public class EditableSocialNetwork
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage="وارد کردن نام الزامی است.")]
        [Display(Name="نام")]
        public string Name { get; set; }

        [Display(Name="آدرس اینترنتی")]
        public string Address { get; set; }

        [Required(ErrorMessage="انتخاب لوگو الزامی است.")]
        [Display(Name="لوگو")]
        public string Image { get; set; }
        
        [Display(Name="آدرس اشتراک گذاری")]
        public string ShareAddress { get; set;}
        
        [Display(Name="قابلیت اشتراک گذاری")]
        public bool IsShareable { get; set; }
        
        [Display(Name="نمایش")]
        public bool Display { get; set; }
    }
}