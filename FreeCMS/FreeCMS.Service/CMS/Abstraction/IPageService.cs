using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Service.CMS.Abstraction
{
	public interface IPageService : IService<Page, int>
	{
		bool PageExists(string title);
		bool PageExists(string title, int id);
		Page FindByTitle(string title);
	}
}
