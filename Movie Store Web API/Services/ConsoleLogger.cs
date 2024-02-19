namespace Movie_Store_Web_Api.Services
{
	public class ConsoleLogger : ILoggerService
	{
		public void Log(string message)
		{
			Console.WriteLine($"[ConsoleLogger]: {message}");
		}
	}
}
