﻿using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Auth.Model;

public class LoginModel
{
	[Required]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
}
