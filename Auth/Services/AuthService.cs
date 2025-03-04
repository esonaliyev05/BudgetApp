using BudgetApp.Auth.Model;
using BudgetApp.Auth.Services.Interface;
using BudgetApp.Data.Components;
using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Data.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Auth.Services;

public class AuthService : IAuthService
{
	private readonly UserManager<User> _userManager;
	readonly ITokenRepository _tokenGenerator;
	public AuthService(UserManager<User> userManager, ITokenRepository tokenGenerator)
	{
		_userManager = userManager;
		_tokenGenerator = tokenGenerator;
	}

	public async ValueTask<TokenModel> Login(LoginModel model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
		{
			throw new BugalteryAPIException(400, "user_not_Found");
		}
		var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
		var roles = await _userManager.GetRolesAsync(user);
		if (!checkPassword)
		{
			throw new BugalteryAPIException(401, "Email or password is incorrect");
		}
		//var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
		var token = _tokenGenerator.CreateToken(user, roles);
		return new TokenModel() { Token = token, Roles = roles.ToArray(), User = new UserModel().MapFromEntity(user) };

	}

	public async ValueTask<UserModel> Registration(RegisterModel user)
	{
		User newUser = new User()
		{
			UserName = user.Username,
			Email = user.Email,
			PasswordHash = user.Password
		};
		var registerUser = await _userManager.CreateAsync(newUser, user.Password);
		if (!registerUser.Succeeded)
		{

			throw new BugalteryAPIException(500, string.Join(", ", registerUser.Errors.Select(x => x.Description)));
		}
		return new UserModel().MapFromEntity(newUser);
	}
}
