// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common.BaseEntities;

namespace CountPad.Domain.Entities.Products
{
    public class Product : BaseAuditableEntity
	{
		public string Name { get; set; }

		public Guid ProductCategoryId { get; set; }
		public ProductCategory ProductCategory { get; set; }

		public string Description { get; set; }
	}
}