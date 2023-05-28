using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Products.Commands.UpdateProduct
{
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty().NotEqual((Guid)default).WithMessage("Product id is required.");
			RuleFor(p => p.Name).NotEmpty().MaximumLength(50).WithMessage("Product name is required.");
			RuleFor(p => p.ProductCategoryId).NotEqual((Guid)default).WithMessage("Product category is required.");
		}
	}
}
