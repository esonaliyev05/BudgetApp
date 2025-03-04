namespace BudgetApp.Data.Pages;

public class PaginationParams
{
	public int PageIndex { get; set; }

	public int PageSize { get; set; }
	public void EnsureOrSetDefaults()
	{
		if (PageSize <= 0)
		{
			PageSize = 10;
		}
		if (PageIndex <= 0)
		{
			PageIndex = 1;
		}
	}
}
