using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticLastVisitors:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticLastVisitors(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            int lastVisitorsCount = 5;
            ViewBag.LastVisitorsCount = lastVisitorsCount;
            var lastVisitors = _viewService.GetLastVisitors(lastVisitorsCount);
            return View("~/Views/Shared/Components/Analytic/AnalyticLastVisitors.cshtml",lastVisitors);
        }
    }
}