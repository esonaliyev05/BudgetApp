
using BudgetApp.Data.Components;
using System.Linq.Expressions;
using BudgetApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Data.Repositories;

//public class GenericRepository<T> : IGenericRepository<T> where T : class
//{
//	private readonly AppDbContext _context;
//	private readonly DbSet<T> _entities;

//	public GenericRepository(AppDbContext context)
//	{
//		_context = context;
//		_entities = context.Set<T>();
//	}

//	public async Task<T> GetById(Guid id)
//	{
//		return await _entities.FindAsync(id);
//	}

//	public async Task<IEnumerable<T>> GetAll()
//	{
//		return await _entities.ToListAsync();
//	}

//	public async Task Add(T entity)
//	{
//		await _entities.AddAsync(entity);
//		await _context.SaveChangesAsync();
//	}

//	public async Task Update(T entity)
//	{
//		_entities.Update(entity);
//		await _context.SaveChangesAsync();
//	}

//	public async Task Delete(Guid id)
//	{
//		var entity = await _entities.FindAsync(id);
//		if (entity != null)
//		{
//			_entities.Remove(entity);
//			await _context.SaveChangesAsync();
//		}
//	}

//	public async Task<IEnumerable<T>> Find(Func<T, bool> predicate)
//	{
//		return await Task.FromResult(_entities.Where(predicate).ToList());
//	}

//	// LEFT JOIN uchun maxsus metod: Transaction, User va Category modellari uchun
//	public async Task<IEnumerable<object>> GetWithLeftJoin<TJoin1, TJoin2>(
//		Expression<Func<T, bool>> predicate,
//		Expression<Func<T, TJoin1, TJoin2, object>> selectExpression)
//		where TJoin1 : class
//		where TJoin2 : class
//	{
//		var query = _entities.AsQueryable();

//		// Dinamik LEFT JOIN: Transaction, User va Category o'rtasidagi bog'liqliklar
//		if (typeof(T) == typeof(Transaction))
//		{
//			var transactionQuery = _context.Set<Transaction>()
//				.Select(t =>new Transaction
//				{
//					Id = t.Id,
//					UserId = t.UserId,
//					CategoryId = t.CategoryId,
//					Amount = t.Amount,
					
					
//				})  // 2️ Obyektlarni Transaction turiga o'tkazamiz
//				.Join(_context.Set<User>(),  // 3️ Transaction va User jadvalini bog'laymiz
//					t => t.UserId,
//					u => u.Id,
//					(t, u) => new { Transaction = t, User = u })
//				.Join(_context.Set<Category>(),  // 4️ Transaction va Category jadvalini bog'laymiz
//					tu => tu.Transaction.CategoryId,
//					c => c.Id,
//					(tu, c) => new { tu.Transaction, tu.User, Category = c });
//			query = (IQueryable<T>)transactionQuery; // 6️ Natijani IQueryable sifatida qaytaramiz
//		}



//		if (predicate != null)
//		{
//			query = query.Where(predicate);
//		}

//		// 🔄 `selectExpression` ni `Expression<Func<T, object>>` formatiga o'tkazamiz
//		var newSelectExpression = Expression.Lambda<Func<T, object>>(
//			Expression.Convert(selectExpression.Body, typeof(object)),
//			selectExpression.Parameters[0]
//		);
//		// SelectExpression orqali natijani shakllantirish
//		var result = await query
//			.Select(selectExpression)
//			.ToListAsync();

//		return result;
//	}
//}