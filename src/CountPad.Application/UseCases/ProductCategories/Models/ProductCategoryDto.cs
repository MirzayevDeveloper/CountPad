using CountPad.Application.Common.Models;
using CountPad.Domain.Entities.Products;

namespace CountPad.Application.UseCases.ProductCategories.Models
{
	public class ProductCategoryDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
	}
}
