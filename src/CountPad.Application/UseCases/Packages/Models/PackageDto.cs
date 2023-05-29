using System;
using System.Text.Json.Serialization;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Application.UseCases.Products.Models;

namespace CountPad.Application.UseCases.Packages.Models
{
	public class PackageDto
	{
		[JsonPropertyName("package_id")]
		public Guid Id { get; set; }
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }
		public ProductDto Product { get; set; }
		public DistributorDto Distributor { get; set; }
	}
}
