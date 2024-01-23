using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Cms.ViewModels.Slide
{
    public class NewSlide
    {
        [Display(Name = "متن")]
        public string Text { get; set; }

        [Required(ErrorMessage = "انتخاب تصویر الزامی است.")]
        [Display(Name = "تصویر")]
        public string Image { get; set; }

        [Display(Name = "آدرس اینترنتی")]
        public string Link { get; set; }

        [Required(ErrorMessage = "مشخص کردن اولویت نمایش الزامی است.")]
        [Display(Name = "اولویت نمایش")]
        public int ShowPriority { get; set; }
    }

}
