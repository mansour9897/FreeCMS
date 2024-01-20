using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using FreeCMS.Service.System.Abstraction;
using FreeCMS.Areas.Core.ViewModels.Analytic;

namespace FreeCMS.Areas.Core.ViewComponents.Analytics
{
    public class AnalyticStatistics:ViewComponent
    {
        private readonly IViewObjectService _viewService;
        public AnalyticStatistics(IViewObjectService viewService)
        {
            _viewService = viewService;
        }
        public IViewComponentResult Invoke()
        {
            StatisticsViewModel vm = new StatisticsViewModel();
            //last three month data
            DateTime lastThreeMonthDate = DateTime.Now.AddDays(-90);
            var lastThreeMonthVisitsData = _viewService.VisitsPerDay(lastThreeMonthDate);
            var lastThreeMonthVisitorsData = _viewService.VisitorsPerDay(lastThreeMonthDate);
            
            //last three month
            ChartModel lastThreeMonthVisits = CreateChartModel("بازدید",lastThreeMonthVisitsData);
            ChartModel lastThreeMonthVisitors = CreateChartModel("بازدید کننده",lastThreeMonthVisitorsData);
            AnalyticChart lastThreeMonthChart = CreateAnalyticChart(lastThreeMonthVisits,lastThreeMonthVisitors);
            vm.LastThreeMonth = lastThreeMonthChart;
            //last month
            ChartModel lastMonthVisits = CreateChartModel("بازدید",lastThreeMonthVisitsData
                .Skip(lastThreeMonthVisitsData.Count - 30)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
            ChartModel lastMonthVisitors = CreateChartModel("بازدید کننده",lastThreeMonthVisitorsData
                .Skip(lastThreeMonthVisitorsData.Count - 30)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
            AnalyticChart lastMonthChart = CreateAnalyticChart(lastMonthVisits,lastMonthVisitors);
            vm.LastMonth = lastMonthChart;
            //last week
            ChartModel lastWeekVisits = CreateChartModel("بازدید",lastThreeMonthVisitsData
                .Skip(lastThreeMonthVisitsData.Count - 7)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
            ChartModel lastWeekVisitors = CreateChartModel("بازدید کننده",lastThreeMonthVisitorsData
                .Skip(lastThreeMonthVisitorsData.Count - 7)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
            AnalyticChart lastWeekChart = CreateAnalyticChart(lastWeekVisits,lastWeekVisitors);
            vm.LastWeek = lastWeekChart;
            return View("~/Views/Shared/Components/Analytic/AnalyticStatistics.cshtml",vm);
        }
        private ChartModel CreateChartModel(string label,Dictionary<string,int> data)
        {
            ChartModel chartModel = new ChartModel();
            chartModel.Label = label;
            chartModel.Labels = data.Keys.ToArray().ToJavascriptArray();
            chartModel.Data = data.Values.ToArray().ToJavascriptArray();
            return chartModel;
        }
        private AnalyticChart CreateAnalyticChart(ChartModel visits,ChartModel visitors)
        {
            AnalyticChart chart = new AnalyticChart();
            chart.Visits = visits;
            chart.Visitors = visitors;
            return chart;
        }
    }
    
    public static class ArrayExtensions
    {
        public static string ToJavascriptArray(this Array array)
        {
            return JsonConvert.SerializeObject(array,Formatting.None);
        }
    }
   
}