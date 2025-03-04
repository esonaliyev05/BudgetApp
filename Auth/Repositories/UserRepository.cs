using BudgetApp.Auth.Model;
using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Data.Common.Repositories;
using BudgetApp.Data.Components;
using BudgetApp.Data.Context;
using BudgetApp.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Auth.Repositories;

public class UserRepository : BaseRepository<User, UserFilterModel>, IUserRepository
{
	private readonly AppDbContext _appDbContext;

	public UserRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<User> Add(User entity)
	{
		var result = await _appDbContext.Users.AddAsync(entity);
		await _appDbContext.SaveChangesAsync();
		return result.Entity;
	}

	public async Task<bool> Delete(Guid id)
	{
		var entity = await _appDbContext.Users
					.Include(u => u.Transactions)  // Include related data for deletion
					.FirstOrDefaultAsync(u => u.Id == id);

		if (entity == null)
		{
			return false;
		}

		_appDbContext.Users.Remove(entity);
		await _appDbContext.SaveChangesAsync();
		return true;
	}

	public async Task<User> GetById(Guid id)
	{
		var userCount = await _appDbContext.Users.CountAsync();
		Console.WriteLine($"Total users in database: {userCount}");
		Console.WriteLine($"GetById called with id: {id}");
		var user= await _appDbContext.Users
					.Include(u => u.Transactions)
						.ThenInclude(t => t.Details)
					.Include(u => u.Transactions)
						.ThenInclude(t => t.Category)
					.FirstOrDefaultAsync(u => u.Id == id);
		if (user == null)
		{
			Console.WriteLine($"User with id {id} not found.");
		}
		else
		{
			Console.WriteLine($"User found: {user.UserName}, Email: {user.Email}");
		}
		return user;
	}

	public async Task<User> Update(User entity)
	{
		var existingUser = await _appDbContext.Users.FindAsync(entity.Id);
		if (existingUser == null)
		{
			throw new BugalteryAPIException(500,"User not found");
		}
		entity.UpdateAt = DateTime.UtcNow;
		_appDbContext.Entry(existingUser).CurrentValues.SetValues(entity);
		await _appDbContext.SaveChangesAsync();
		return existingUser;
	}
	public async Task<decimal> GetUserBalance(Guid userId)
	{
		return await _appDbContext.Transactions
			.Where(t => t.UserId == userId)
			.SumAsync(t => t.Amount);
	}

	protected override IQueryable<User> GetQuery(UserFilterModel model)
	{
		var query = _appDbContext.Users.Include(x=>x.Transactions).AsNoTracking();
		if(model.Id.HasValue && model.Id.Value != Guid.Empty)
		{
			query = query.Where(x=>x.Id == model.Id);
		}
		if(!string.IsNullOrEmpty(model.Username) && !string.IsNullOrWhiteSpace(model.Username))
		{
			query=query.Where(x=>EF.Functions.Like(x.UserName.ToLower(), $"%{model.Username.Trim().ToLower()}%"));
		}
		return query;
	}
}
