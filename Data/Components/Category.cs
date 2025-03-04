using BudgetApp.Data.Components.Commons;

namespace BudgetApp.Data.Components;

public class Category : Auditable
{
	public string Name { get; set; }
	public ICollection<Transaction> Transactions { get; set; }  
}
