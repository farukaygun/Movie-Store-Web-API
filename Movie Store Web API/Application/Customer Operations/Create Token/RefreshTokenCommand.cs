using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Token_Operations;

namespace Movie_Store_Web_API.Application.Customer_Operations.Create_Token
{
	public class RefreshTokenCommand(IMovieStoreDbContext context,
		IConfiguration configuration,
		string refreshToken)
	{
		public string RefreshToken { get; set; } = refreshToken;
		public async Task<TokenModel> Handle()
		{
			var user = await context.Customers.SingleOrDefaultAsync(c => c.RefreshToken == RefreshToken);

			if (user is not null)
			{
				var handler = new TokenHandler(configuration);
				var token = handler.CreateAccessToken();

				user.RefreshToken = token.RefreshToken!;
				user.RefreshTokenExpiryTime = token.Expiration.AddMinutes(60);

				await context.SaveChangesAsync();

				return token;
			}
			else
				throw new InvalidOperationException("Invalid refresh token!");
		}
	}
}
