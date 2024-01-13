﻿using System.ComponentModel.DataAnnotations;

namespace FreeCMS.Areas.Cms.ViewModels.Post
{
	public class EditablePost
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModified { get; set; }
		public int CountView { get; set; }
		public int AuthorId { get; set; }
		[Required(ErrorMessage = "وارد کردن عنوان الزامی است.")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }

		[Required(ErrorMessage = "وارد کردن خلاصه برای نوشته الزامی است.")]
		[MaxLength(150, ErrorMessage = "خلاصه باید حاوی 150 کاراکتر باشد.")]
		[Display(Name = "خلاصه")]
		public string Summary { get; set; }

		[UIHint("tinymce-advanced")]
		[DataType(DataType.MultilineText)]
		[Display(Name = "متن")]
		public string Body { get; set; }
		[Display(Name = "فعال سازی نظرات")]
		public bool EnableComment { get; set; }
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

		[Display(Name = "دسته ها")]
		public int[] SelectedTopics { get; set; }
	}
}
