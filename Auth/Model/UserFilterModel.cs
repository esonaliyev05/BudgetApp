using BudgetApp.Data.Pages;

namespace BudgetApp.Auth.Model
{
	public class UserFilterModel: PaginationParams
	{
		public Guid? Id { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
	}
}
