using BudgetApp.Auth.Model;

namespace BudgetApp.Auth.Services.Interface
{
	public interface IAuthService
	{
		ValueTask<UserModel> Registration(RegisterModel user);
		ValueTask<TokenModel> Login(LoginModel model);
	}
}
