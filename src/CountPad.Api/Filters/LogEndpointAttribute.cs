using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace CountPad.Api.Filters
{
	public class LogEndpointAttribute : TypeFilterAttribute
	{
		public LogEndpointAttribute() : base(typeof(LogEndpointFilter))
		{
		}

		private class LogEndpointFilter : IActionFilter
		{
			public async void OnActionExecuting(ActionExecutingContext context)
			{
				var request = context.HttpContext.Request;
				Log.Information("Request - Method: {Method}, Path: {Path}, Body: {Body}",
					request.Method, request.Path, await GetRequestBodyAsString(request));
			}

			public async void OnActionExecuted(ActionExecutedContext context)
			{
				var response = context.HttpContext.Response;
				Log.Information("Response - Status: {StatusCode}, Body: {Body}",
					response.StatusCode, await GetResponseBodyAsString(response));
			}

			private async ValueTask<string> GetRequestBodyAsString(HttpRequest request)
			{
				request.EnableBuffering();
				request.Body.Position = 0;
				using (var reader = new StreamReader(request.Body, leaveOpen: true))
				{
					string body = await reader.ReadToEndAsync();
					request.Body.Position = 0;
					return body;
				}
			}

			private async Task<string> GetResponseBodyAsString(HttpResponse response)
			{
				var originalBody = response.Body;
				try
				{
					using (var memoryStream = new MemoryStream())
					{
						response.Body = memoryStream;

						await response.Body.CopyToAsync(memoryStream);
						response.Body.Seek(0, SeekOrigin.Begin);

						using (var reader = new StreamReader(response.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true))
						{
							string body = await reader.ReadToEndAsync();
							response.Body.Seek(0, SeekOrigin.Begin);
							return body;
						}
					}
				}
				finally
				{
					response.Body = originalBody;
				}
			}

		}
	}
}
