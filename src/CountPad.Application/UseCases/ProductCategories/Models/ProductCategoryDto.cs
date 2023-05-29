using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.ProductCategories.Models
{
	public class ProductCategoryDto
	{
		[JsonPropertyName("product_category_id")]
		public string Name { get; set; }
	}
}
