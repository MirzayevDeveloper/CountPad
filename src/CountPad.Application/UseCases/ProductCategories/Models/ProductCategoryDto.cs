using System;
using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.ProductCategories.Models
{
	public class ProductCategoryDto
	{
        [JsonPropertyName("product_category_id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
	}
}
