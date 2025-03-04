using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Data.Components;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetApp.Auth.Repositories
{
	public class TokenRepository: ITokenRepository
	{
		readonly IConfiguration _configuration;

		public TokenRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateToken(User user, IList<string> roles)
		{
			List<Claim> claims = new List<Claim>()
		{
			new(ClaimTypes.NameIdentifier,user.Id.ToString()),
			new(ClaimTypes.Email,user?.Email)
		};
			foreach (var role in roles)
			{
				claims.Add(new(ClaimTypes.Role, role));
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddDays(5),
				signingCredentials: creds
				);
			string jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
	}
}
