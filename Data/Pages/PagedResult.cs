namespace BudgetApp.Data.Pages;

public static class PagedResult
{
	public static PagedResult<T> Create<T>(IList<T> Items, int CurrentPage, int PageSize, int TotalCount)
	{
		var totalPage = TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
		return new PagedResult<T>(Items, CurrentPage, PageSize, TotalCount, totalPage);
	}
}
public record PagedResult<T>(IList<T> Items, int CurrentPage, int PageSize, int TotalCount, int TotalPage);
