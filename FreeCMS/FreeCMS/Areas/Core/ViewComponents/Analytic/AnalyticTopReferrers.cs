using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticTopReferrers:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticTopReferrers(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            int topReferrersCount = 5;
            ViewBag.TopReferrersCount = topReferrersCount;
            var topPages = _viewService.GetTopReferrers(topReferrersCount);
            return View("~/Views/Shared/Components/Analytic/AnalyticTopReferrers.cshtml",topPages);
        }
    }
}