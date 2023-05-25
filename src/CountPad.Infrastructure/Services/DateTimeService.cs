using System;
using CountPad.Application.Common.Interfaces;

namespace CountPad.Infrastructure.Services
{
	public class DateTimeService : IDateTime
	{
		public DateTimeOffset Now => DateTimeOffset.Now;
	}
}
