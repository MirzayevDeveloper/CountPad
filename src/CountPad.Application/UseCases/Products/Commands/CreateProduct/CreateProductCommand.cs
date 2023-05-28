using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.Products.Commands.CreateProduct
{
	public class CreateProductCommand : IRequest<ProductDto>
	{
		public string Name { get; set; }
		public Guid ProductCategoryId { get; set; }
		public string Description { get; set; }
	}

	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateProductCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			Product maybeProduct =
				_context.Products.SingleOrDefault(p => p.Name.Equals(request.Name));

			ValidateProductIsNull(request, maybeProduct);

			ProductCategory category =
				_context.ProductCategories.SingleOrDefault(
					pc => pc.Id.Equals(request.ProductCategoryId));

			ValidateCategoryIsNotNull(request, category);

			var product = new Product
			{
				Name = request.Name,
				ProductCategory = category,
				Description = request.Description,
				ProductCategoryId = category.Id,
			};

			maybeProduct = _context.Products.Add(product).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductDto>(maybeProduct);
		}

		private static void ValidateCategoryIsNotNull(CreateProductCommand request, ProductCategory category)
		{
			if (category == null)
			{
				throw new NotFoundException(
					nameof(ProductCategory), request.ProductCategoryId);
			}
		}

		private static void ValidateProductIsNull(CreateProductCommand request, Product maybeProduct)
		{
			if (maybeProduct != null)
			{
				throw new AlreadyExistsException(nameof(Product), request.Name);
			}
		}
	}
}
