namespace BudgetApp.Data.Exceptions;

public class BugalteryAPIException : Exception
{
	public int Code { get; set; }
	public bool? Global { get; set; } 
	public BugalteryAPIException(int code,string message,bool? global=true) : base(message)
	{
		Code = code;
		Global = global;
	}
}
