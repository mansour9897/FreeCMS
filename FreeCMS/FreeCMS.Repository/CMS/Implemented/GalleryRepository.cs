using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class GalleryRepository : BaseRepository<Gallery, int>, IGalleryRepository
	{
		public GalleryRepository(FreeCMSContext context)
			: base(context) { }
	}
}
