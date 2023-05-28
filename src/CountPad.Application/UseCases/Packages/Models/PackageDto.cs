using System;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Application.UseCases.Products.Models;

namespace CountPad.Application.UseCases.Packages.Models
{
	public class PackageDto : BaseAuditableEntityDto
	{
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }
		public ProductDto Product { get; set; }
		public DistributorDto Distributor { get; set; }
	}
}
