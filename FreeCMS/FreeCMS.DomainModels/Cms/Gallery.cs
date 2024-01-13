using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FreeCMS.DomainModels.Cms
{
	public enum GalleryType
	{
		[Display(Name = "ویدئو")]
		Video,
		[Display(Name = "تصویر")]
		Image
	}


	public class Gallery : SeoBase
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public DateTime CreationDate { get; set; }
		public string? Logo { get; set; }
		public GalleryType Type { get; set; }
		public virtual ICollection<GalleryItem> Items { get; set; }
	}
}
