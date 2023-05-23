// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using CountPad.Domain.Common;

namespace CountPad.Domain.Entities.Products
{
	public class ProductCategory : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

