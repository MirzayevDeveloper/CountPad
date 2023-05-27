using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.ProductCategories.Commands.CreateProductCategory
{
	public class CreateProductCategoryCommand : IRequest<ProductCategoryDto>
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value.ToLower(); }
		}
	}

	public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, ProductCategoryDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateProductCategoryCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ProductCategoryDto> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
		{
			ProductCategory maybeProductCategory =
				_context.ProductCategories.SingleOrDefault(p => p.Name.Equals(request.Name));

			ValidateCategoryDoesNotExist(request, maybeProductCategory);

			maybeProductCategory = _context.ProductCategories.Add(new()
			{ Name = request.Name }).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ProductCategoryDto>(maybeProductCategory);
		}

		private static void ValidateCategoryDoesNotExist(CreateProductCategoryCommand request, ProductCategory maybeProductCategory)
		{
			if (maybeProductCategory != null)
			{
				throw new AlreadyExistsException(nameof(ProductCategory), request.Name);
			}
		}
	}
}
