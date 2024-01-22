using FreeCMS.DomainModels.System;
using FreeCMS.Service.System.Abstraction;
using System.Linq.Expressions;
using FreeCMS.Repository.System;

namespace FreeCMS.Service.System.Implemented
{
    public class ViewObjectService : IViewObjectService
    {
        #region variables
        private readonly IViewObjectRepository _repository;
        #endregion

        #region constructors
        public ViewObjectService(IViewObjectRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region methods
        public ViewObject Get(Guid id)
        {
            return _repository.Get(id);
        }
        public IList<ViewObject> List()
        {
            return _repository.List();
        }
        public IList<ViewObject> List(Expression<Func<ViewObject, bool>> expression)
        {
            return _repository.List(expression);
        }
        public ViewObject Insert(ViewObject entity)
        {
            return _repository.Insert(entity);
        }
        public ViewObject Update(ViewObject entity)
        {
            return _repository.Update(entity);
        }
        public ViewObject Delete(Guid id)
        {
            ViewObject viewObject = Get(id);
            if (viewObject != null)
                return Delete(viewObject);
            return null;
        }
        public ViewObject Delete(ViewObject entity)
        {
            return _repository.Delete(entity);
        }
        public void Delete(Expression<Func<ViewObject, bool>> expression)
        {
            _repository.Delete(expression);
        }
        #endregion

        #region utility methods
        public int OnlineVisitors(int minutes)
        {
            DateTime now = DateTime.Now.AddMinutes(-1 * minutes);
            return _repository.List(v => v.Date >= now).GroupBy(v => v.VisitorId).Select(v => v.FirstOrDefault())
                .Distinct().ToList().Count;
        }
        public int VisitorsCountByDate(DateTime date)
        {
            return _repository.List(v => v.Date.Date == date.Date).GroupBy(v => v.VisitorId).Distinct().ToList().Count;
        }
        public int VisitorsCountFromDate(DateTime fromDate)
        {
            return _repository.List(v => v.Date.Date >= fromDate.Date).GroupBy(v => v.VisitorId).Distinct().ToList().Count;
        }
        public int VisitsCountByDate(DateTime date)
        {
            return _repository.List(v => v.Date.Date == date.Date).Count;
        }
        public int VisitsCountFromDate(DateTime fromDate)
        {
            return _repository.List(v => v.Date.Date >= fromDate.Date).Count;
        }
        public IList<string> GetTopPaths(int count)
        {
            return _repository.List().GroupBy(v => v.Path).Select(v => new { path = v.Key, count = v.Count() })
                .OrderByDescending(t => t.count).Select(t => t.path).Take(count).ToList();
        }
        public IList<string> GetTopReferrers(int count)
        {
            return _repository.List().GroupBy(v => v.Referrer).Select(v => new { path = v.Key, count = v.Count() })
                .OrderByDescending(t => t.count).Select(t => t.path).Take(count).ToList();
        }
        public IEnumerable<string> GetLastVisitors(int count)
        {
            return _repository.List().OrderByDescending(v => v.Date).GroupBy(v => v.VisitorId).Select(v => v.FirstOrDefault().Ip)
                .Distinct().Take(count).ToList();
        }
        public Dictionary<string, int> VisitsPerDay(DateTime fromDate)
        {
            List<DateTime> datesRange = GetDateRange(fromDate, DateTime.Now);
            //get visits per day
            var visits = _repository.List(v => v.Date.Date >= fromDate.Date).OrderByDescending(v => v.Date).GroupBy(v => v.Date.Date)
                    .Select(v => new { v.Key.Date, Count = v.Count() }).ToList();


            //return result by using left outer join 
            return datesRange.GroupJoin(visits, dateRange => dateRange.Date, visit => visit.Date.Date,
                (dateRange, visit) => new { Date = dateRange.Date, Visits = visit })
                .SelectMany(d => d.Visits.DefaultIfEmpty(new { Date = d.Date, Count = 0 }),
                (d, v) => new { Date = d.Date, Count = v.Count })
                .ToDictionary(d => d.Date.ToShortDateString(), d => d.Count);
        }
        public Dictionary<string, int> VisitorsPerDay(DateTime fromDate)
        {
            List<DateTime> datesRange = GetDateRange(fromDate, DateTime.Now);
            //get unique visitors count per day
            var visitors = _repository.List(v => v.Date.Date >= fromDate.Date).OrderByDescending(v => v.Date)
                    .GroupBy(v => new { v.VisitorId, v.Date.Date })
                    .Select(v => new { Id = v.Key.VisitorId, Date = v.Key.Date, Count = v.Count() }).Distinct()
                    .GroupBy(v => v.Date)
                    .Select(g => new { g.Key.Date, Count = g.Count() }).ToList();

            return datesRange.GroupJoin(visitors, dateRange => dateRange.Date, visit => visit.Date.Date,
                (dateRange, visit) => new { Date = dateRange.Date, Visits = visit })
                .SelectMany(d => d.Visits.DefaultIfEmpty(new { Date = d.Date, Count = 0 }),
                (d, v) => new { Date = d.Date, Count = v.Count })
                .ToDictionary(d => d.Date.ToShortDateString(), d => d.Count);
        }
        public Dictionary<string, int> GetBrowserUsage()
        {
            return _repository.List().GroupBy(v => v.Browser).Select(v => new { v.Key, Count = v.Count() })
                .ToDictionary(d => d.Key, d => d.Count);
        }
        public Dictionary<string, int> GetOSUsage()
        {
            return _repository.List().GroupBy(v => v.OS).Select(v => new { v.Key, Count = v.Count() })
                .ToDictionary(d => d.Key, d => d.Count);
        }
        #endregion

        #region private methods
        private List<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(1, endDate.Subtract(startDate).Days)
                .Select(i => startDate.AddDays(i)).ToList();
        }
        #endregion
    }
    public static class DateExtensions
    {
        public static string ToJavaScriptTimeStamp(this DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }
    }
}