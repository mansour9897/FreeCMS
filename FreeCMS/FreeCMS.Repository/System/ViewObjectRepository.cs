using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using System.Linq.Expressions;

namespace FreeCMS.Repository.System
{
    public class ViewObjectRepository:BaseRepository<ViewObject,Guid>,IViewObjectRepository
    {
        private readonly FreeCMSContext _context;
        public ViewObjectRepository(FreeCMSContext uow)
            :base(uow)
        {
            _context = uow;
        }
        public void Delete(Expression<Func<ViewObject,bool>> expression)
        {
            var visits = _context.Set<ViewObject>().Where(expression).ToList();
            _context.Set<ViewObject>().RemoveRange(visits);
            _context.SaveChanges();
        }
    }
}