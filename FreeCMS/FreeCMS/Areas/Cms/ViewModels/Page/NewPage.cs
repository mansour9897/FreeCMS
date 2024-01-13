using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FreeCMS.Areas.Cms.ViewModels.Page
{
	public class NewPage
	{
		[Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }


		[Display(Name = "متن کامل")]
		[DataType(DataType.MultilineText)]
		[AllowHtml]
		[UIHint("CkEditor")]
		public string Body { get; set; }
		[Display(Name = "انتشار")]
		public bool IsPublished { get; set; }

		[Required(ErrorMessage = "انتخاب تصویر الزامی است.")]
		[Display(Name = "تصویر")]
		public string Image { get; set; }
		[Required(ErrorMessage = "وارد کردن کلمات کلیدی الزامی است.")]
		[Display(Name = "کلمات کلیدی")]
		public string MetaKeywords { get; set; }

		[Required(ErrorMessage = "وارد کردن متای توضیحات الزامی است.")]
		[MaxLength(150, ErrorMessage = "متای توضیحات باید حاوی 150 کاراکتر باشد.")]
		[Display(Name = "متای توضیحات")]
		public string MetaDescription { get; set; }
	}
}
