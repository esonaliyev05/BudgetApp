using System.Linq.Dynamic.Core;
using BudgetApp.Auth.Model;
using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Auth.Services.Interface;
using BudgetApp.Data.Components;
using BudgetApp.Data.Exceptions;
using BudgetApp.Data.Pages;
using BudgetApp.Data.Repositories;

namespace BudgetApp.Auth.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async ValueTask<UserModel> Add(UserCreateModel entity)
	{
		try
		{
			var createUser = entity.ToEntity();
			createUser.CreatedAt = DateTime.Now;
			createUser.UpdateAt = DateTime.Now;
			await _userRepository.Add(createUser);
			return new UserModel().MapFromEntity(createUser);
		}
		catch (BugalteryAPIException e)
		{
			throw new BugalteryAPIException(500, $"{e.Message}");
		}
	}

	public async ValueTask<bool> Delete(Guid id)
	{
		await _userRepository.Delete(id);
		return true;
	}


	public async ValueTask<UserModel> GetById(Guid id)
	{
		return new UserModel().MapFromEntity(await _userRepository.GetById(id));
	}

	public async ValueTask Update(UserUpdateModel entity)
	{
		try
		{
			var updateUser = entity.ToEntity();
			updateUser.UpdateAt = DateTime.Now;
			await _userRepository.Update(updateUser);
		}
		catch (BugalteryAPIException be)
		{
			throw new BugalteryAPIException(500, $"{be.ToString}");
		}
	}
	public async ValueTask<Data.Pages.PagedResult<UserModel>> GetAll(UserFilterModel filter)
	{
		var count = await _userRepository.GetCount(filter);
		var list = await _userRepository.GetByFilter(filter);
		return Data.Pages.PagedResult.Create(list.Select(p => new UserModel().MapFromEntity(p)).ToList(), filter.PageIndex, filter.PageSize, count);
	}
}
