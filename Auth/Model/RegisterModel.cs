using System.ComponentModel.DataAnnotations;
using BudgetApp.Data.Components;

namespace BudgetApp.Auth.Model;

public class RegisterModel
{
	[Required]
	public string Username { get; set; }
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }

	public static RegisterModel FromUserCreateModel(UserCreateModel model)
	{
		return new RegisterModel
		{
			Username = model.Username,
			Email = model.Email,
			Password = model.Password,
		};
	}
}
