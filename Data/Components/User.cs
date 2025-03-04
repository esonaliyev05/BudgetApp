using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Data.Components;

public class User : IdentityUser<Guid>
{
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdateAt { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
}
