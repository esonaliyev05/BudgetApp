//using BudgetApp.Data.Repositories;
//using System.Linq.Expressions;

//namespace BudgetApp.Data.Services;

//public class Service<T> : IService<T> where T : class
//{
//	private readonly IGenericRepository<T> _repository;

//	public Service(IGenericRepository<T> repository)
//	{
//		_repository = repository;
//	}

//	public async Task<T> GetById(Guid id)
//	{
//		return await _repository.GetById(id);
//	}

//	public async Task<IEnumerable<T>> GetAll()
//	{
//		return await _repository.GetAll();
//	}

//	public async Task Add(T entity)
//	{
//		await _repository.Add(entity);
//	}

//	public async Task Update(T entity)
//	{
//		await _repository.Update(entity);
//	}

//	public async Task Delete(Guid id)
//	{
//		await _repository.Delete(id);
//	}

//	public async Task<IEnumerable<T>> Find(Func<T, bool> predicate)
//	{
//		return await _repository.Find(predicate);
//	}

//	public async Task<IEnumerable<object>> GetWithLeftJoin<TJoin1, TJoin2>(
//		Expression<Func<T, bool>> predicate,
//		Expression<Func<T, TJoin1, TJoin2, object>> selectExpression)
//		where TJoin1 : class
//		where TJoin2 : class
//	{
//		return await _repository.GetWithLeftJoin(predicate, selectExpression);
//	}
//}
