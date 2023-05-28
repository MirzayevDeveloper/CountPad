using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Packages.Commands.UpdatePackage
{
	public class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
	{
		public UpdatePackageCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty()
				.NotEqual((Guid)default)
				.WithMessage("Id is required.");

			RuleFor(p => p.ProductId).NotEmpty()
				.NotEqual((Guid)default)
				.WithMessage("Product id is required.");

			RuleFor(p => p.DistributorId).NotEmpty()
				.NotEqual((Guid)default)
				.WithMessage("Product id is required.");

			RuleFor(p => p.Count).NotEqual((double)default)
				.GreaterThan(0).WithMessage("Package count is required.");

			RuleFor(p => p.SalePrice).NotEqual((decimal)default)
				.GreaterThan(0).WithMessage("Package sale price is required");

			RuleFor(p => p.IncomingPrice).NotEqual((decimal)default)
				.GreaterThan(0).WithMessage("Package incoming price is required");

			RuleFor(p => p.IncomingDate).NotEqual((DateTimeOffset)default)
				.WithMessage("Package incoming date is required.");
		}
	}
}
