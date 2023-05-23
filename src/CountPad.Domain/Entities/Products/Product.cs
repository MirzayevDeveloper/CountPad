// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;

namespace CountPad.Domain.Entities.Products
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ProductCategory ProductType { get; set; }
		public string Description { get; set; }
	}
}