using FreeCMS.Common.Repository;
using System.Linq.Expressions;

namespace FreeCMS.Common.Service
{
	public abstract class BaseService<T, TId> : IService<T, TId>
		where T : class
		where TId : struct
	{
		private readonly IRepository<T, TId> _repo;
		public BaseService(IRepository<T, TId> repo)
		{
			this._repo = repo;
		}
		public virtual List<T> GetAll()
		{
			return _repo.List() as List<T>;
		}
		public virtual IList<T> List(Expression<Func<T, bool>> expression)
		{
			return _repo.List(expression);
		}
		public virtual T FindById(TId id)
		{
			return _repo.Get(id);
		}
		public virtual T Add(T model)
		{
			return _repo.Insert(model);
		}
		public virtual bool Delete(T model)
		{
			if (_repo.Delete(model) != null)
				return true;
			return false;
		}
		public virtual bool Delete(TId id)
		{
			T tempModel = _repo.Get(id);
			if (tempModel != null)
			{
				return this.Delete(tempModel);
			}
			return false;
		}
		public virtual bool Update(T model)
		{
			if (_repo.Update(model) != null)
				return true;
			return false;
		}
	}
}
