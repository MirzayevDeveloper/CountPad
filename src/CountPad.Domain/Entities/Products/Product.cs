// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common;

namespace CountPad.Domain.Entities.Products
{
	public class Product : BaseAuditableEntity
	{
		public string Name { get; set; }

        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductType { get; set; }

		public string Description { get; set; }
	}
}