/*using BudgetApp.Data.Components;
using BudgetApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Data.Controller;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
	private readonly IService<Transaction> _transactionService;

	public TransactionsController(IService<Transaction> transactionService)
	{
		_transactionService = transactionService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllTransactions()
	{
		// LEFT JOIN orqali Transaction, User va Category ma'lumotlarini olish
		var transactions = await _transactionService.GetWithLeftJoin<User, Category>(
			predicate: null, // Barcha tranzaksiyalarni olish
			selectExpression: (t, u, c) => new
			{
				Transaction = t,
				UserName = u != null ? u.UserName : "No User",
				CategoryName = c != null ? c.Name : "No Category"
			});

		return Ok(transactions);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetTransaction(Guid id)
	{
		var transaction = await _transactionService.GetById(id);
		if (transaction == null)
			return NotFound();

		var detailedTransaction = await _transactionService.GetWithLeftJoin<User, Category>(
			t => t.Id == id,
			(t, u, c) => new
			{
				Transaction = t,
				UserName = u != null ? u.UserName : "No User",
				CategoryName = c != null ? c.Name : "No Category"
			});

		return Ok(detailedTransaction.FirstOrDefault());
	}
}*/