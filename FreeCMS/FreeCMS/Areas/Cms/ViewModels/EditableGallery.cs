using FreeCMS.DomainModels.Cms;
using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Cms.ViewModels
{
	public class EditableGallery
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public GalleryType Type { get; set; }

		[Required(ErrorMessage = "وارد کردن نام الزامی است.")]
		[Display(Name = "نام")]
		public string Name { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Required(ErrorMessage = "انتخاب لوگو الزامی است.")]
		[Display(Name = "لوگو")]
		public string Logo { get; set; }

		[Required(ErrorMessage = "وارد کردن کلمات کلیدی الزامی است.")]
		[Display(Name = "کلمات کلیدی")]
		public string MetaKeywords { get; set; }

		[Required(ErrorMessage = "وارد کردن متای توضیحات الزامی است.")]
		[MaxLength(150, ErrorMessage = "متای توضیحات باید حاوی 150 کاراکتر باشد.")]
		[Display(Name = "متای توضیحات")]
		public string MetaDescription { get; set; }
	}
}
