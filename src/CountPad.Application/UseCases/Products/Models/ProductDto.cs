using System;
using System.Text.Json.Serialization;
using CountPad.Application.UseCases.ProductCategories.Models;

namespace CountPad.Application.UseCases.Products.Models
{
	public class ProductDto
	{
		[JsonPropertyName("product_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ProductCategoryDto ProductCategory { get; set; }
		public string Description { get; set; }
	}
}
