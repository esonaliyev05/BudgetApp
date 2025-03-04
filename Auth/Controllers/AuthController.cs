using BudgetApp.Auth.Model;
using BudgetApp.Auth.Services.Interface;
using BudgetApp.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}
	[HttpPost("Login")]
	[Consumes("application/json")] // JSON formatini aniq belgilash
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async ValueTask<IActionResult> Login([FromBody] LoginModel model)
	{
		try
		{

			return Ok(await _authService.Login(model));
		}
		catch (BugalteryAPIException ex)
		{
			return BadRequest(new
			{
				global = ex.Message,
			});
		}
	}
	[HttpPost("register")]
	[Consumes("application/json")] // JSON formatini aniq belgilash
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async ValueTask<IActionResult> Register([FromBody] RegisterModel model)
	{
		try
		{

			return Ok(await _authService.Registration(model));
		}
		catch (BugalteryAPIException ex)
		{
			return BadRequest(new
			{
				global = ex.Message,
			});
		}
	}
}
