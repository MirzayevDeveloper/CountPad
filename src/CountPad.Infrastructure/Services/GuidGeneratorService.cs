using System;
using CountPad.Application.Common.Interfaces;

namespace CountPad.Infrastructure.Services
{
	public class GuidGeneratorService : IGuidGenerator
	{
		public Guid Guid => Guid.NewGuid();
	}
}
