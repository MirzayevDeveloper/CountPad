using System;
using FluentValidation;

namespace CountPad.Application.UseCases.Products.Commands.CreateProduct
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(p => p.Name).NotEmpty().MaximumLength(50).WithMessage("Product name is required.");
			RuleFor(p => p.ProductCategoryId).NotEqual((Guid)default).WithMessage("Product category is required.");
		}
	}
}
