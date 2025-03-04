namespace BudgetApp.Data.Models;

public class ResponseModel<T>
{
	public bool Status { get; set; }
	public string Error { get; set; }
	public T Data { get; set; }
	public bool? GlobalError { get; set; } = null;
}
public class ResponseModel
{
	public static ResponseModel<U> Create<U>(U model, bool status, bool global, string error)
	{
		return new ResponseModel<U> { Data = model, Status = status, Error = error, GlobalError = global };
	}
}
