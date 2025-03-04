using BudgetApp.Data.Components;

namespace BudgetApp.Auth.Model;

public class UserModel
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdateAt { get; set; }
	public string Password { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public virtual UserModel MapFromEntity(User entity)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity), "User entity cannot be null.");
		}
		Id = entity.Id;
		CreatedAt = entity.CreatedAt;
		UpdateAt = entity.UpdateAt == DateTime.MinValue ? null : entity.UpdateAt;
		Username = entity.UserName;
		Email = entity.Email;
		Password = entity.PasswordHash;
		Transactions = entity.Transactions is not null? entity.Transactions.ToList() : new List<Transaction>();
		return this;
	}

}
