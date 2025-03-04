using BudgetApp.Data.Components;
using BudgetApp.Data.Components.Commons;
using BudgetApp.Data.Pages;

namespace BudgetApp.Data.Common.Repositories.Interfaces;

public interface IBaseRepository<TEntity, TFilter> where TEntity : User where TFilter : PaginationParams
{
	Task<List<TEntity>> GetByFilter(TFilter model, string[] includes = null);
}
