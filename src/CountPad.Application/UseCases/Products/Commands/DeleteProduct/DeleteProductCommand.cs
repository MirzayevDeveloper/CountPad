using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.Products.Commands.DeleteProduct
{
	public record DeleteProductCommand(Guid productId) : IRequest<ProductDto>;

	public class DeleteProductCommandHander : IRequestHandler<DeleteProductCommand, ProductDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteProductCommandHander(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			Product maybeProduct = await
				_context.Products.FindAsync(new object[] { request.productId });

			ValidateProductIsNotNull(request, maybeProduct);

			_context.Products.Remove(maybeProduct);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductDto>(maybeProduct);
		}

		private static void ValidateProductIsNotNull(DeleteProductCommand request, Product maybeProduct)
		{
			if (maybeProduct is null)
			{
				throw new NotFoundException(nameof(Product), request.productId);
			}
		}
	}
}
