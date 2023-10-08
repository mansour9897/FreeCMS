using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
	public class PageService : BaseService<Page, int>, IPageService
	{
		private readonly IPageRepository _repo;
		public PageService(IPageRepository repository)
			: base(repository)
		{
			this._repo = repository;
		}

		public override Page Add(Page page)
		{
			page.CreationDate = DateTime.Now;
			page.LastModified = DateTime.Now;
			return base.Add(page);
		}
		public override bool Update(Page page)
		{
			page.LastModified = DateTime.Now;
			return base.Update(page);
		}


		public bool PageExists(string title)
		{
			if (this.List(t => t.Title == title).FirstOrDefault() != null)
				return true;
			return false;
		}
		public bool PageExists(string title, int id)
		{
			if (this.List(t => t.Id != id && t.Title == title).FirstOrDefault() != null)
			{
				return true;
			}
			return false;
		}
		public Page FindByTitle(string title)
		{
			return this.List(p => p.Title == title).FirstOrDefault();
		}
	}
}
