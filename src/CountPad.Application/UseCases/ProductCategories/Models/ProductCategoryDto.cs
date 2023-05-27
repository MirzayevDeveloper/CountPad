using CountPad.Application.Common.Models;

namespace CountPad.Application.UseCases.ProductCategories.Models
{
	public class ProductCategoryDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
	}
}
