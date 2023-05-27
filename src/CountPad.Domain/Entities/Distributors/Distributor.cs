// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using CountPad.Domain.Common.BaseEntities;

namespace CountPad.Domain.Entities
{
	public class Distributor : BaseAuditableEntity
	{
		public string Name { get; set; }
		public string CompanyPhone { get; set; }
		public string DelivererPhone { get; set; }
	}
}