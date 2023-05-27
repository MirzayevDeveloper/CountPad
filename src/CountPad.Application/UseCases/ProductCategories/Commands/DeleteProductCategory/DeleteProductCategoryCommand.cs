using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.ProductCategories.Commands.UpdateProductCategory;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.ProductCategories.Commands.DeleteProductCategory
{
	public record DeleteProductCategoryCommand(Guid categoryId) : IRequest<ProductCategoryDto>;

	public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, ProductCategoryDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteProductCategoryCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductCategoryDto> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
		{
			ProductCategory maybeProductCategory =
				_context.ProductCategories.SingleOrDefault(pc => pc.Equals(request.categoryId));

			ValidateCategoryIsNotNull(request, maybeProductCategory);

			maybeProductCategory =
				_context.ProductCategories.Remove(maybeProductCategory).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductCategoryDto>(maybeProductCategory);
		}

		private static void ValidateCategoryIsNotNull(DeleteProductCategoryCommand request, ProductCategory maybeProductCategory)
		{
			if (maybeProductCategory == null)
			{
				throw new NotFoundException(nameof(ProductCategory), request.categoryId);
			}
		}
	}
}
