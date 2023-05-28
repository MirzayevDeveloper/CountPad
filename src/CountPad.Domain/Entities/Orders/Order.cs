// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Users;

namespace CountPad.Domain.Entities.Orders
{
	public class Order : BaseAuditableEntity
	{
		public Guid PackageId { get; set; }
		public Package Package { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }

		public double Count { get; set; }
		public double SoldPrice { get; set; }
		public DateTimeOffset SoldDate { get; set; }
	}
}
