using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Service.CMS.Abstraction
{
	public interface IGalleryItemService : IService<GalleryItem, int>
	{
		List<GalleryItem> GetByGalleryType(GalleryType type);
	}
}
