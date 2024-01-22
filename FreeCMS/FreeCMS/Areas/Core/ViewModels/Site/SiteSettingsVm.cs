using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels.Site
{
    public class SiteSettingsVm
    {
        public SiteSettingsVm(){}
        public SiteSettingsVm(string name,string email,string telephone,string address,string slogan,
            string logo,string fax,string aboutUs)
        {
            this.Name = name;
            this.Email = email;
            this.Telephone = telephone;
            this.Address = address;
            this.Slogan = slogan;
            //this.Logo = logo;
            this.AboutUs = aboutUs;
            this.Fax = fax;
        }
        [Display(Name="نام سایت")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage="ایمیل وارد شده معتبر نمی باشد.")]
        [Display(Name="ایمیل")]
        public string Email { get; set; }
        [Display(Name="تلفن")]
        public string Telephone { get; set; }
        [Display(Name="آدرس")]
        public string Address { get; set; }
        [Display(Name="شعار")]
        public string Slogan { get; set; }
        //[Display(Name="لوگو")]
        //public string Logo { get; set; }
        [Display(Name="فکس")]
        public string Fax { get; set; }
        [UIHint("tinymce-basic")]
        [DataType(DataType.MultilineText)]
        [Display(Name="درباره ما")]
        public string AboutUs { get; set; }
        
    }
    
}