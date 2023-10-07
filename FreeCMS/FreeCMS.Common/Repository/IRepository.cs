using System.Linq.Expressions;

namespace FreeCMS.Common.Repository
{
	public interface IRepository<T, TId>
	 where T : class
	 where TId : struct
	{
		T Get(TId id);
		IList<T> List();
		IList<T> List(Expression<Func<T, bool>> expression);
		T Insert(T entity);
		T Update(T entity);
		T Delete(T entity);

	}
}
