using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Service.CMS.Abstraction
{
	public interface IGalleryService : IService<Gallery, int>
	{
		List<Gallery> GetByType(GalleryType type);
		Gallery FindByNameAndType(string name, GalleryType type);
		bool Exists(string name, GalleryType type);
		bool Exists(int id, string name, GalleryType type);
	}
}
