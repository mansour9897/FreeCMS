using FreeCMS.Common.Repository;
using FreeCMS.DomainModels.System;
using System.Linq.Expressions;
namespace FreeCMS.Repository.System
{
    public interface IViewObjectRepository : IRepository<ViewObject, Guid>
    {
        void Delete(Expression<Func<ViewObject, bool>> expression);
    }
}