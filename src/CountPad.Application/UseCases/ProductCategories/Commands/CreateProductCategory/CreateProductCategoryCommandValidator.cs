using FluentValidation;

namespace CountPad.Application.UseCases.ProductCategories.Commands.CreateProductCategory
{
	public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
	{
		public CreateProductCategoryCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("ProductCategory is invalid.");
		}
	}
}
