using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Products.Queries.GetProducts
{
	public record GetProductsQuery : IRequest<ProductDto[]>;

	public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductsQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
		{
			Product[] products = await _context.Products.ToArrayAsync();

			return _mapper.Map<ProductDto[]>(products);
		}
	}
}
