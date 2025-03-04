using BudgetApp.Auth.Model;
using BudgetApp.Data.Common.Repositories.Interfaces;
using BudgetApp.Data.Components;

namespace BudgetApp.Auth.Repositories.Interface;

public interface IUserRepository : IBaseRepository<User, UserFilterModel>
{
	Task<User> GetById(Guid id);
	Task<List<User>> GetByFilter(UserFilterModel model, string[] includes = null);
	Task<User> Add(User entity);
	Task<User> Update(User entity);
	Task<bool> Delete(Guid id);
	Task<int> GetCount(UserFilterModel model);
}
