namespace CountPad.Api.Filters
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	public class AddCustomHeaderAttribute : ResultFilterAttribute
	{
		private readonly string _headerName;
		private readonly string _headerValue;

		public AddCustomHeaderAttribute(string headerName, string headerValue)
		{
			_headerName = headerName;
			_headerValue = headerValue;
		}

		public override void OnResultExecuting(ResultExecutingContext context)
		{
			if (context.Result is ObjectResult)
			{
				var response = context.HttpContext.Response;
				response.Headers.Add(_headerName, _headerValue);
			}

			base.OnResultExecuting(context);
		}
	}
}
