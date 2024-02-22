using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Movie_Store_Web_API.Token_Operations
{
	public class TokenHandler(IConfiguration configuration)
	{
		public TokenModel CreateAccessToken()
		{
			var tokenModel = new TokenModel();
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			tokenModel.Expiration = DateTime.Now.AddMinutes(15);
			var jwtSecurityToken = new JwtSecurityToken(
				issuer: configuration["Token:Issuer"],
				audience: configuration["Token:Audience"],
				expires: tokenModel.Expiration,
				notBefore: DateTime.Now,
				signingCredentials: credentials
			);

			var handler = new JwtSecurityTokenHandler();

			tokenModel.AccessToken = handler.WriteToken(jwtSecurityToken);
			tokenModel.RefreshToken = CreateRefreshToken();

			return tokenModel;
		}

		private static string CreateRefreshToken()
		{
			return Guid.NewGuid().ToString();
		}
	}

	public class TokenModel
	{
		public string? AccessToken { get; set; }
		public DateTime Expiration { get; set; }
		public string? RefreshToken { get; set; }
	}
}
