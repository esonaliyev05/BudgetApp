using BudgetApp.Data.Components;

namespace BudgetApp.Auth.Repositories.Interface;

public interface ITokenRepository
{
	string CreateToken(User user, IList<string> roles);
}
