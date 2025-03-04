using BudgetApp.Data.Components.Commons;

namespace BudgetApp.Data.Components;

public class Transaction: Auditable
{
	public Guid UserId { get; set; }
	public Guid CategoryId { get; set; }
	public decimal Amount { get; set; }
	public DateTime TransactionDate { get; set; }
	public User User { get; set; } 
	public Category Category { get; set; } 
	public ICollection<Details> Details { get; set; } 
}
