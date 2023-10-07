using FreeCMS.DAL;
using System.Linq.Expressions;

namespace FreeCMS.Common.Repository
{
	public abstract class BaseRepository<T, TId> : IRepository<T, TId>
		where T : class
		where TId : struct
	{
		private readonly FreeCMSContext _context;
		public BaseRepository(FreeCMSContext context)
		{
			_context = context;
		}
		public T Get(TId id)
		{
			return _context.Set<T>().Find(id);
		}
		public IList<T> List()
		{
			return _context.Set<T>().ToList();
		}
		public IList<T> List(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).ToList();
		}
		public T Insert(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
			return entity;
		}
		public T Update(T entity)
		{
			_context.MarkAsChanged<T>(entity);
			_context.SaveAllChanges();
			return entity;
		}
		public T Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			_context.SaveAllChanges();
			return entity;
		}
	}
}
