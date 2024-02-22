using Microsoft.EntityFrameworkCore;
using Movie_Store_Web_API.Db_Operations;
using Movie_Store_Web_API.Token_Operations;

namespace Movie_Store_Web_API.Application.Customer_Operations.Create_Token
{
	public class CreateTokenCommand(IMovieStoreDbContext context,
		IConfiguration configuration,
		CreateTokenModel model)
	{
		public async Task<TokenModel> Handle()
		{
			var user = await context.Customers.SingleOrDefaultAsync(c => c.Email == model.Email && c.Password == model.Password);

			if (user is not null)
			{
				var handler = new TokenHandler(configuration);
				var token = handler.CreateAccessToken();

				user.RefreshToken = token.RefreshToken;
				user.RefreshTokenExpiryTime = token.Expiration.AddMinutes(30);

				await context.SaveChangesAsync();

				return token;
			}
			
			throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı!");
		}
	}

	public class CreateTokenModel
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
