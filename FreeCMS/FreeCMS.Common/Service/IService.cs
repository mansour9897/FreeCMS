using System.Linq.Expressions;

namespace FreeCMS.Common.Service
{
	public interface IService<T, TId>
		where T : class
		where TId : struct
	{
		List<T> GetAll();
		IList<T> List(Expression<Func<T, bool>> expression);
		T FindById(TId id);
		T Add(T model);
		bool Delete(T model);
		bool Delete(TId id);
		bool Update(T model);
	}
}
