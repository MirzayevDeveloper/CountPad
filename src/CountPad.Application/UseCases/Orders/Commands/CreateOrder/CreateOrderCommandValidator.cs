using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(o => o.PackageId).NotEqual((Guid)default).WithMessage("Package id required.");
			RuleFor(o => o.UserId).NotEqual((Guid)default).WithMessage("User id required.");
			RuleFor(o => o.SoldPrice).GreaterThan(0).WithMessage("Sold price is required.");
			RuleFor(o => o.Count).GreaterThan(0).WithMessage("Count is required");
		}
	}
}
