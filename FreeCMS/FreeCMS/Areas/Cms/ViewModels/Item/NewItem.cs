using FreeCMS.DomainModels.Cms;
using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Cms.ViewModels.Item
{
	public class NewItem
	{
		[Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Required(ErrorMessage = "وارد کردن آدرس الزامی است.")]
		[Display(Name = "آدرس")]
		public string Address { get; set; }

		[Required(ErrorMessage = "انتخاب گالری الزامی است.")]
		[Display(Name = "گالری")]
		public int GalleryId { get; set; }
		public GalleryType Type { get; set; }
	}
}
