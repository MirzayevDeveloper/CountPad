using System;
using FluentValidation;

namespace CountPad.Application.UseCases.ProductCategories.Commands.UpdateProductCategory
{
	public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
	{
		public UpdateProductCategoryCommandValidator()
		{
			RuleFor(pc => pc.Name).NotEmpty().WithMessage("ProductCategory is invalid.");
			RuleFor(pc => pc.Id).NotEqual((Guid)default).WithMessage("ProductCategory is required");
		}
	}
}
