using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
    public class SlideService : BaseService<Slide, int>, ISlideService
    {
        public SlideService(ISlideRepository slideRepository)
            : base(slideRepository) { }

        public override Slide Add(Slide slide)
        {
            slide.CreationDate = DateTime.Now;
            return base.Add(slide);
        }
    }

}
