using BudgetApp.Data.Common.Repositories.Interfaces;
using BudgetApp.Data.Components;
using BudgetApp.Data.Components.Commons;
using BudgetApp.Data.Pages;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace BudgetApp.Data.Common.Repositories;

public abstract class BaseRepository<TEntity, TFilter> : IBaseRepository<TEntity, TFilter> where TEntity : User where TFilter : PaginationParams
{
	public virtual async Task<List<TEntity>> GetByFilter(TFilter model, string[] includes = null)
	{
		var query = GetQuery(model);
		if (includes is not null && includes.Length > 0)
		{
			foreach (var include in includes)
			{
				query = query.Include(include);
			}

		}
		model.EnsureOrSetDefaults();
		query = query.Skip(model.PageSize * (model.PageIndex - 1)).Take(model.PageSize);

		return await query.ToListAsync();
	}

	public async Task<int> GetCount(TFilter model)
	{
		var query = GetQuery(model);
		return await query.CountAsync();
	}

	protected abstract IQueryable<TEntity> GetQuery(TFilter model);
}
