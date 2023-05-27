using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.ProductCategories.Queries.GetProductCategories
{
	public record GetProductCategoriesQuery : IRequest<ProductCategoryDto[]>;

	public class GetProductCategoriesHandler : IRequestHandler<GetProductCategoriesQuery, ProductCategoryDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductCategoriesHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductCategoryDto[]> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
		{
			ProductCategory[] categories = await _context.ProductCategories.ToArrayAsync();

			return _mapper.Map<ProductCategoryDto[]>(categories);
		}
	}
}
