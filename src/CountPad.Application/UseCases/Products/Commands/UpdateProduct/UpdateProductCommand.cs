using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest<ProductDto>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid ProductCategoryId { get; set; }
		public string Description { get; set; }
	}

	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateProductCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			Product maybeProduct = await
				_context.Products.FindAsync(new object[] { request.Id });

			ValidateProductIsNotNull(request, maybeProduct);

			ProductCategory maybeCategory = await
				_context.ProductCategories.FindAsync(new object[] { request.ProductCategoryId });

			ValidateCategoryIsNotNull(request, maybeCategory);

			maybeProduct.Name = request.Name;
			maybeProduct.ProductCategory = maybeCategory;
			maybeProduct.Description = request.Description;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductDto>(maybeProduct);
		}

		private static void ValidateCategoryIsNotNull(UpdateProductCommand request, ProductCategory maybeCategory)
		{
			if (maybeCategory is null)
			{
				throw new NotFoundException(nameof(ProductCategory), request.ProductCategoryId);
			}
		}

		private static void ValidateProductIsNotNull(UpdateProductCommand request, Product maybeProduct)
		{
			if (maybeProduct is null)
			{
				throw new NotFoundException(nameof(Product), request.Id);
			}
		}
	}
}
