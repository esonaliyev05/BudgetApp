using BudgetApp.Auth.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.Data.Components;
using BudgetApp.Auth.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BudgetApp.Auth.Repositories.Interface;

namespace BudgetApp.Auth.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly IAuthService _authService;
	private readonly IUserService _userRepository;

	public UserController(UserManager<User> userManager, IAuthService authService, IUserService userRepository)
	{
		_userManager = userManager;
		_authService = authService;
		_userRepository = userRepository;
	}
	[HttpGet("{id}")]
	public async ValueTask<IActionResult> GetAsync([FromRoute]Guid id)
	{

		return Ok( await _userRepository.GetById(id));
	}
	[HttpGet("AllUser")]
	public async ValueTask<IActionResult> GetAll([FromQuery]UserFilterModel model)
	{
		
		return Ok(await _userRepository.GetAll(model));
	}
	[HttpPost]
	[Authorize]
	public async ValueTask<IActionResult> CreateAsync(UserCreateModel model)
	{
		var registerModel = RegisterModel.FromUserCreateModel(model);
		return Ok(await _authService.Registration(registerModel));
	}
	[HttpPut]
	[Authorize]
	public async ValueTask<IActionResult> UpdateAsync(UserUpdateModel model)
	{
		return Ok( _userRepository.Update(model));
	}
	[HttpDelete("{id}")]
	//[Authorize]
	public async ValueTask<IActionResult> DeleteAsync([FromRoute] Guid id)
	{
		return Ok(await _userRepository.Delete(id));
	}
}
