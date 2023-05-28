using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderCommandValidator()
		{
			RuleFor(o => o.Id).NotEqual((Guid)default).WithMessage("Order id required.");
			RuleFor(o => o.PackageId).NotEqual((Guid)default).WithMessage("Package id required.");
			RuleFor(o => o.UserId).NotEqual((Guid)default).WithMessage("User id required.");
			RuleFor(o => o.SoldPrice).GreaterThan(0).WithMessage("Sold price is required.");
			RuleFor(o => o.Count).GreaterThan(0).WithMessage("Count is required");
		}
	}
}
