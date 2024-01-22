using FreeCMS.DomainModels.System;
using System.Linq.Expressions;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IViewObjectService
    {
        ViewObject Get(Guid id);
        IList<ViewObject> List();
        IList<ViewObject> List(Expression<Func<ViewObject,bool>> expression);
        ViewObject Insert(ViewObject entity);
        ViewObject Update(ViewObject entity);
        ViewObject Delete(Guid id);
        ViewObject Delete(ViewObject entity);
        void Delete(Expression<Func<ViewObject,bool>> expression);
        /***       utility methods       ***/
        int OnlineVisitors(int minutes);
        int VisitorsCountByDate(DateTime date);
        int VisitorsCountFromDate(DateTime fromDate);
        int VisitsCountByDate(DateTime date);
        int VisitsCountFromDate(DateTime fromDate);
        IList<string> GetTopPaths(int count);
        IList<string> GetTopReferrers(int count);
        IEnumerable<string> GetLastVisitors(int count);
        Dictionary<string,int> VisitsPerDay(DateTime fromDate);
        Dictionary<string,int> VisitorsPerDay(DateTime fromDate);
        Dictionary<string,int> GetBrowserUsage();
        Dictionary<string,int> GetOSUsage();
    }
}