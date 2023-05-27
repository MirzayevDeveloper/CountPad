using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace CountPad.Application.Common.Behaviours
{
	public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			try
			{
				return await next();
			}
			catch (Exception ex)
			{
				var requestName = typeof(TRequest).Name;

				Log.Error(ex, $"CountPad Request: Unhandled Exception for Request {requestName} {request}\n");

				throw;
			}
		}
	}
}
