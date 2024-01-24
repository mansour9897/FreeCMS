using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.ViewComponenets.Slider
{
    public class ShowSlider: ViewComponent
    {
        private readonly ISlideService _slideService;
        public ShowSlider(ISlideService slideService)
        {
            _slideService = slideService;
        }
        public IViewComponentResult Invoke()
        {
            var slides = _slideService.GetAll().OrderBy(s => s.ShowPriority).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/Slide/ShowSlider.cshtml", slides);
        }

    }
}
