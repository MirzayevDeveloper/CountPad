using FluentValidation;

namespace CountPad.Application.UseCases.Permissions.Commands.CreatePermission
{
	public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
	{
		public CreatePermissionValidator()
		{
			RuleFor(v => v.PermissionName)
				.MaximumLength(50)
				.NotEmpty()
				.NotNull()
				.WithMessage("Permission name is required.");
		}
	}
}
