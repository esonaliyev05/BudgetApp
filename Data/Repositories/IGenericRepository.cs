using System.Linq.Expressions;

namespace BudgetApp.Data.Repositories;

public interface IGenericRepository<T> where T : class
{
	Task<T> GetById(Guid id);
	Task<IEnumerable<T>> GetAll();
	Task Add(T entity);
	Task Update(T entity);
	Task Delete(Guid id);
	Task<IEnumerable<T>> Find(Func<T, bool> predicate); // Oddiy qidiruv

	// LEFT JOIN uchun maxsus metod: bir nechta entity'larni birlashtirish
	Task<IEnumerable<object>> GetWithLeftJoin<TJoin1, TJoin2>(
		Expression<Func<T, bool>> predicate,
		Expression<Func<T, TJoin1, TJoin2, object>> selectExpression)
		where TJoin1 : class
		where TJoin2 : class;
}