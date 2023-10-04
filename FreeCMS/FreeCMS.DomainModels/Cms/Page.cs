using FreeCMS.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.DomainModels.Cms
{
	public class Page : SeoBase
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModified { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public int CountView { get; set; }
		public bool IsPublished { get; set; }
		public string Image { get; set; }

		public int AuthorId { get; set; }
		public virtual ApplicationUser Author { get; set; }
	}
}
