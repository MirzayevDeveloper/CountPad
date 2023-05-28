using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.Products.Queries.GetProducts
{
	public record GetProductQuery(Guid productId) : IRequest<ProductDto>;

	public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			Product maybeProduct = await
				_context.Products.FindAsync(new object[] { request.productId });

			ValidateProductIsNotNull(request, maybeProduct);

			return _mapper.Map<ProductDto>(maybeProduct);
		}

		private static void ValidateProductIsNotNull(GetProductQuery request, Product maybeProduct)
		{
			if (maybeProduct is null)
			{
				throw new NotFoundException(nameof(Product), request.productId);
			}
		}
	}
}
