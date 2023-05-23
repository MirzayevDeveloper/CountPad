// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;

namespace CountPad.Domain.Common
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdateDate { get; set; }
	}
}
