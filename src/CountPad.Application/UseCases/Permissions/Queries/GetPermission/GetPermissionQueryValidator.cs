using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Permissions.Queries.GetPermission
{
	public class GetPermissionQueryValidator : AbstractValidator<GetPermissionQuery>
	{
		public GetPermissionQueryValidator() 
		{
			RuleFor(v => v.permissionId)
				.NotEqual(default(Guid))
				.WithMessage("Permission id is required.");
		}
	}
}
