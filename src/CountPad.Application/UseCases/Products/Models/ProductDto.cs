using System;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Domain.Entities.Products;

namespace CountPad.Application.UseCases.Products.Models
{
	public class ProductDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
		public ProductCategoryDto ProductCategory { get; set; }
		public string Description { get; set; }
	}
}
