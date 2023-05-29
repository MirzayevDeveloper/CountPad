using System;
using System.Text.Json.Serialization;
using CountPad.Application.UseCases.Packages.Models;

namespace CountPad.Application.UseCases.Orders.Models
{
	public class OrderDto
	{
		[JsonPropertyName("order_id")]
		public Guid Id { get; set; }
		public PackageDto Package { get; set; }
		public UserOrderDto User { get; set; }
		public double Count { get; set; }
		public double SoldPrice { get; set; }
		public DateTimeOffset SoldDate { get; set; }
	}
}
