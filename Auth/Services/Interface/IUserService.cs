using BudgetApp.Auth.Model;
using BudgetApp.Data.Components;
using BudgetApp.Data.Pages;

namespace BudgetApp.Auth.Services.Interface;

public interface IUserService
{
	ValueTask<UserModel> GetById(Guid id);
	ValueTask<PagedResult<UserModel>> GetAll(UserFilterModel filter);
	ValueTask<UserModel> Add(UserCreateModel entity);
	ValueTask Update(UserUpdateModel entity);
	ValueTask<bool> Delete(Guid id);
}
