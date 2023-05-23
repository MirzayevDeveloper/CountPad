﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common;
using CountPad.Domain.Entities.Users;

namespace CountPad.Domain.Entities.Solds
{
	public class Sold : BaseEntity
	{
		public DateTime SoldDate { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
