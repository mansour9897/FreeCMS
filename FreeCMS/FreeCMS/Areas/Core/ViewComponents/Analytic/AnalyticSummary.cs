using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticSummary:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticSummary(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            
            AnalyticSummaryViewModel vm = FillSummary();
            return View("~/Views/Shared/Components/Analytic/AnalyticSummary.cshtml",vm);
        }
        private AnalyticSummaryViewModel FillSummary()
        {   
            AnalyticSummaryViewModel vm = new AnalyticSummaryViewModel();
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime lastWeek = DateTime.Now.AddDays(-7);
            DateTime lastMonth = DateTime.Now.AddDays(-30);

            
            vm.OnlineUsersCount = _viewService.OnlineVisitors(5);

            vm.TodayVisitorsCount = _viewService.VisitorsCountByDate(today);
            vm.TodayVisitsCount = _viewService.VisitsCountByDate(today);

            vm.YesterdayVisitorsCount = _viewService.VisitorsCountByDate(yesterday);
            vm.YesterdayVisitsCount = _viewService.VisitsCountByDate(yesterday);

            vm.LastWeekVisitorsCount = _viewService.VisitorsCountFromDate(lastWeek);
            vm.LastWeekVisitsCount = _viewService.VisitsCountFromDate(lastWeek);

            vm.LastMonthVisitorsCount = _viewService.VisitorsCountFromDate(lastMonth);
            vm.LastMonthVisitsCount = _viewService.VisitsCountFromDate(lastMonth);
            return vm;
        }
    }
    public class AnalyticSummaryViewModel
    {
        public int OnlineUsersCount { get; set; }
        public int TodayVisitorsCount { get; set; }
        public int TodayVisitsCount { get; set; }
        public int YesterdayVisitorsCount { get; set; }
        public int YesterdayVisitsCount { get; set; }
        public int LastWeekVisitorsCount { get; set; }
        public int LastWeekVisitsCount { get; set; }
        public int LastMonthVisitorsCount { get; set; }
        public int LastMonthVisitsCount { get; set; }
    }
}