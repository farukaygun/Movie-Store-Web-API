namespace Movie_Store_Web_Api.Services
{
	public class DbLogger : ILoggerService
	{
		public void Log(string message)
		{
			Console.WriteLine($"[DbLogger]: {message}");
		}
	}
}
