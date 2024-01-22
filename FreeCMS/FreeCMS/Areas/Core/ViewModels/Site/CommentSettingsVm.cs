using System.ComponentModel.DataAnnotations;
namespace FreeCMS.Areas.Core.ViewModels.Site
{
    public class CommentSettingsVm
    {
        [Display(Name="تایید خودکار دیدگاه ها")]
        public bool AutoVerify { get; set; }
    }
}