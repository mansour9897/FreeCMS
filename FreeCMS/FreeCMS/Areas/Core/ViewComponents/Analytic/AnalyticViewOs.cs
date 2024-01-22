using FreeCMS.Areas.Core.ViewModels.Analytic;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticViewOs:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticViewOs(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            Dictionary<string,int> os = _viewService.GetOSUsage();
            ChartModel osChart = new ChartModel();
            osChart.Label = "سیستم عامل ها";
            osChart.Labels = JsonConvert.SerializeObject(os.Keys.ToArray(),Formatting.None);
            osChart.Data = JsonConvert.SerializeObject(os.Values.ToArray(),Formatting.None);
            return View("~/Views/Shared/Components/Analytic/AnalyticViewOs.cshtml",osChart);
        }
    }
}