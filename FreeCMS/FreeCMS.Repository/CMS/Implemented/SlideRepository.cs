using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class SlideRepository : BaseRepository<Slide, int>, ISlideRepository
	{
		public SlideRepository(FreeCMSContext context) : base(context) { }
	}
}
