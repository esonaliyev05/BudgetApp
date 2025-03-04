namespace BudgetApp.Auth.Model
{
	public class TokenModel
	{
		public string Token { get; set; }
		public string[] Roles { get; set; }
		public UserModel User { get; set; }
	}
}
