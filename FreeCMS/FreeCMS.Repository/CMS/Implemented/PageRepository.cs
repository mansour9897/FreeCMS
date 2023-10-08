using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class PageRepository : BaseRepository<Page, int>, IPageRepository
	{
		public PageRepository(FreeCMSContext context) : base(context) { }
	}
}
