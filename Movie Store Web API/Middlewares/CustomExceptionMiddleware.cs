using Movie_Store_Web_Api.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Movie_Store_Web_Api.Middlewares
{
	public class CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
	{
		private readonly RequestDelegate next = next;
		private readonly ILoggerService loggerService = loggerService;

		public async Task Invoke(HttpContext context)
		{
			var watch = Stopwatch.StartNew();

			try
			{
				var message = $"[Request] HTTP\tMethod:{context.Request.Method}\tPath: {context.Request.Path}\tStatus Code: {context.Response.StatusCode}\tResponse Time: {watch.ElapsedMilliseconds} ms";
				loggerService.Log(message);

				await next(context);

				watch.Stop();

				message = $"[Response] HTTP\tMethod:{context.Request.Method}\tPath: {context.Request.Path}\tStatus Code: {context.Response.StatusCode}\tResponse Time: {watch.ElapsedMilliseconds} ms";
				loggerService.Log(message);
			}
			catch (Exception e)
			{
				watch.Stop();
				await HandleException(context, e, watch);
			}
		}

		private Task HandleException(HttpContext context, Exception e, Stopwatch watch)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			string message = $"[Error] HTTP\tMethod:{context.Request.Method}\tStatus Code: {context.Response.StatusCode}\tError: {e.Message}\tResponse Time: {watch.ElapsedMilliseconds} ms";
			loggerService.Log(message);

			var result = JsonConvert.SerializeObject(new { error = e.Message }, Formatting.None);
			return context.Response.WriteAsync(result);
		}
	}

	public static class CustomExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionMiddleware>();
		}
	}
}
