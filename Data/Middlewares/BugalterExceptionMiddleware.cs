using BudgetApp.Data.Exceptions;
using BudgetApp.Data.Models;

namespace BudgetApp.Data.Middlewares;

public class BugalterExceptionMiddleware
{
	private readonly RequestDelegate _requestDelegate;

	private readonly ILogger<BugalterExceptionMiddleware> _logger;
	public BugalterExceptionMiddleware(RequestDelegate requestDelegate, ILogger<BugalterExceptionMiddleware> logger)
	{
		_requestDelegate = requestDelegate;
		_logger = logger;
	}
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _requestDelegate.Invoke(context);
		}
		catch (BugalteryAPIException ex)
		{
			HandleException(context, ex.Code, ex.Message, ex.Global);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.ToString());
			await HandleException(context, 500,$"{ex.Message}",true);
		}
	}
	public async Task HandleException(HttpContext context,int code, string message,bool? Global)
	{
		context.Response.StatusCode = code;
		await context.Response.WriteAsJsonAsync(
			new ResponseModel<string>
			{
				Status=false,
				Error=message,
				Data = null,
				GlobalError = Global
			});
	}

}
