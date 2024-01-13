using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Cms.ViewModels.Topic
{
	public class NewTopic
	{
		[Required(ErrorMessage = "وارد کردن نام الزامی است.")]
		[Display(Name = "نام")]
		public string Name { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Display(Name = "والد")]
		public int? ParentId { get; set; }
	}
}
