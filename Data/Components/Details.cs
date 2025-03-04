using BudgetApp.Data.Components.Commons;

namespace BudgetApp.Data.Components;

public class Details : Auditable
{
	public Guid TransactionId { get; set; }
	public string Description { get; set; }
	public string Status { get; set; }
	public Transaction Transaction { get; set; } 
}
