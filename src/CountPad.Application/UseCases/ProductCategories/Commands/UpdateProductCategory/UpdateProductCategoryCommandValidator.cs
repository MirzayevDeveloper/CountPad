using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
