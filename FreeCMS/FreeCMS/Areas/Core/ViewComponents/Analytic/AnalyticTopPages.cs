using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticTopPages:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticTopPages(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            int topPagesCount = 5;
            ViewBag.TopPagesCount = topPagesCount;
            var topPages = _viewService.GetTopPaths(topPagesCount);
            return View("~/Views/Shared/Components/Analytic/AnalyticTopPages.cshtml",topPages);
        }
    }
}