using System;

namespace CountPad.Domain.Common.BaseEntities
{
	public abstract class BaseAuditableEntity : BaseEntity
	{
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
