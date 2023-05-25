using FluentValidation;

namespace CountPad.Application.UseCases.Permissions.Commands.UpdatePermission
{
	public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
	{
		public UpdatePermissionCommandValidator()
		{
			RuleFor(v => v.PermissionName)
				.MaximumLength(50)
				.NotEmpty()
				.WithMessage("Permission name is required.");
		}
	}
}
