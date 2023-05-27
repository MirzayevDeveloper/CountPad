// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Solds;

namespace CountPad.Domain.Entities.Orders
{
    public class Order : BaseAuditableEntity
	{
		public Guid PackageId { get; set; }
		public Package Package { get; set; }

		public Guid SoldId { get; set; }
		public Sold Sold { get; set; }
	}
}
