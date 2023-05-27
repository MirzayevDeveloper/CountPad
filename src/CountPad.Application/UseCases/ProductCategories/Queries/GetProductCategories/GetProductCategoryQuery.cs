using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.ProductCategories.Queries.GetProductCategories
{
	public record GetProductCategoryQuery(Guid categoryId) : IRequest<ProductCategoryDto>;

	public class GetProductCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, ProductCategoryDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductCategoryQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductCategoryDto> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
		{
			ProductCategory maybeProductCategory = await
				_context.ProductCategories.SingleOrDefaultAsync(pc => pc.Equals(request.categoryId));

			ValidateCategoryIsNotNull(request, maybeProductCategory);

			return _mapper.Map<ProductCategoryDto>(maybeProductCategory);
		}

		private static void ValidateCategoryIsNotNull(GetProductCategoryQuery request, ProductCategory maybeProductCategory)
		{
			if (maybeProductCategory == null)
			{
				throw new NotFoundException(nameof(ProductCategory), request.categoryId);
			}
		}
	}
}
