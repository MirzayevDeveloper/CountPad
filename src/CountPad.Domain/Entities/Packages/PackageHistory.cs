using System;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Products;

namespace CountPad.Domain.Entities.Packages
{
	public class PackageHistory : BaseAuditableEntity
	{
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }
		public Product Product { get; set; }
		public Distributor Distributor { get; set; }
	}
}
