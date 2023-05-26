using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Roles.Commands.CreateRole
{
	public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
	{
		public CreateRoleCommandValidator()
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
