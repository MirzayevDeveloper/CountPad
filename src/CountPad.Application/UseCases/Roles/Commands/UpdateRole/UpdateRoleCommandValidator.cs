using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Roles.Commands.UpdateRole
{
	public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
	{
		public UpdateRoleCommandValidator()
		{
			RuleFor(r => r.RoleName)
				.NotEmpty()
				.NotNull()
				.WithMessage("Name is requied");

			RuleFor(r => r.Permissions)
				.ForEach(x => x.NotEqual((Guid)default))
				.WithMessage("Id is required");
		}
	}
}
