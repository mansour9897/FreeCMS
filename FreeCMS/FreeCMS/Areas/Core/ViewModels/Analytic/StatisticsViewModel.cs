namespace FreeCMS.Areas.Core.ViewModels.Analytic
{
    public class StatisticsViewModel
    {
        public AnalyticChart LastWeek { get; set; }
        public AnalyticChart LastMonth { get; set; }
        public AnalyticChart LastThreeMonth { get; set; }
    }

    public class AnalyticChart
    {
        public ChartModel Visits { get; set; }
        public ChartModel Visitors { get; set; }
    }
    public class ChartModel
    {
        public string Label { get; set; }
        public string Labels { get; set; }
        public string Data { get; set; }
    }
}
