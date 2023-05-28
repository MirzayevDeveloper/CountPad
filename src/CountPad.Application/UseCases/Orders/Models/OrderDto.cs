using System;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Application.UseCases.Users.Models;

namespace CountPad.Application.UseCases.Orders.Models
{
	public class OrderDto : BaseAuditableEntityDto
	{
		public PackageDto Package { get; set; }
		public UserDto User { get; set; }
		public double Count { get; set; }
		public double SoldPrice { get; set; }
		public DateTimeOffset SoldDate { get; set; }
	}
}
