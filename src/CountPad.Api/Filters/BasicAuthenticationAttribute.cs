using Microsoft.AspNetCore.Authorization;

namespace CountPad.Api.Filters
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class BasicAuthenticationAttribute : Attribute, IAuthorizeData
	{
		public BasicAuthenticationAttribute() { }

		public BasicAuthenticationAttribute(string policy)
		{
			Policy = policy;
		}

		public string? AuthenticationSchemes { get; set; }
		public string? Policy { get; set; }
		public string? Roles { get; set; }
	}
}
