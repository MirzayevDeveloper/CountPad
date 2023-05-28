// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Products;

namespace CountPad.Domain.Entities.Packages
{
	public class Package : BaseAuditableEntity
	{
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }

		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid DistributorId { get; set; }
		public Distributor Distributor { get; set; }
	}
}
