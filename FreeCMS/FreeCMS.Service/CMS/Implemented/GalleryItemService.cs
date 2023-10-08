using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
	public class GalleryItemService : BaseService<GalleryItem, int>, IGalleryItemService
	{
		private readonly IGalleryRepository _galleryRepository;
		public GalleryItemService(IGalleryItemRepository repository, IGalleryRepository galleryRepository)
			: base(repository)
		{
			_galleryRepository = galleryRepository;
		}

		public override GalleryItem Add(GalleryItem item)
		{
			item.CreationDate = DateTime.Now;
			return base.Add(item);
		}
		public List<GalleryItem> GetByGalleryType(GalleryType type)
		{
			//find all gallery ids with specific type
			var galleries = _galleryRepository.List(g => g.Type == type).Select(g => g.Id).ToList();
			return this.GetAll().Where(gi => galleries.Contains(gi.GalleryId)).ToList();
		}
	}
}
