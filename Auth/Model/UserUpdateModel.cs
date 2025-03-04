using System.ComponentModel.DataAnnotations;
using BudgetApp.Data.Components;
namespace BudgetApp.Auth.Model;

public class UserUpdateModel
{
	public Guid Id { get; set; }
	[Required]
	public string Username { get; set; }
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public User ToEntity()
	{
		var updateEntity = new User()
		{
			Id = this.Id,
			UserName = this.Username,
			Email = this.Email,
			PasswordHash = this.Password,
			Transactions = this.Transactions is not null?Transactions.ToList() : new List<Transaction>()
		};
		return updateEntity;
	}
}
