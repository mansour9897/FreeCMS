using FreeCMS.Areas.Core.ViewModels.Analytic;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticViewBrowsers:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticViewBrowsers(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            Dictionary<string,int> browsers = _viewService.GetBrowserUsage();
            ChartModel browserChart = new ChartModel();
            browserChart.Label = "مرورگرها";
            browserChart.Labels = JsonConvert.SerializeObject(browsers.Keys.ToArray(),Formatting.None);
            browserChart.Data = JsonConvert.SerializeObject(browsers.Values.ToArray(),Formatting.None);
            return View("~/Views/Shared/Components/Analytic/AnalyticViewBrowsers.cshtml",browserChart);
        }
    }
}