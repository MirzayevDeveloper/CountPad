using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.ProductCategories.Commands.UpdateProductCategory
{
	public class UpdateProductCategoryCommand : IRequest<ProductCategoryDto>
	{
		private string _name;

		public Guid Id { get; set; }
		public string Name
		{
			get { return _name; }
			set { _name = value.ToLower(); }
		}
	}

	public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, ProductCategoryDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateProductCategoryCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductCategoryDto> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
		{
			ProductCategory maybeProductCategory =
				_context.ProductCategories.SingleOrDefault(pc => pc.Id.Equals(request.Id));

			ValidateCategoryIsNotNull(request, maybeProductCategory);

			maybeProductCategory.Name = request.Name;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductCategoryDto>(maybeProductCategory);
		}

		private static void ValidateCategoryIsNotNull(UpdateProductCategoryCommand request, ProductCategory maybeProductCategory)
		{
			if (maybeProductCategory == null)
			{
				throw new NotFoundException(nameof(ProductCategory), request.Id);
			}
		}
	}
}
