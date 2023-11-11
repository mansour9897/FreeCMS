namespace FreeCMS.Areas.Cms.ViewModels.Topic
{
	public class EditableTopic
	{
		public int Id { get; set; }
		[Display(Name = "تاریخ ایجاد")]
		public DateTime CreationDate { get; set; }
		[Display(Name = "آخرین ویرایش")]
		public DateTime LastModified { get; set; }
		[Required(ErrorMessage = "وارد کردن نام الزامی است.")]
		[Display(Name = "نام")]
		public string Name { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Display(Name = "والد")]
		public int? ParentId { get; set; }
		public Topic Parent { get; set; }
	}
}
