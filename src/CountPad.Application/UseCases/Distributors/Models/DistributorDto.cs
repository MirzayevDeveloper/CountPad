using System;
using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.Distributors.Models
{
	public class DistributorDto
	{
		[JsonPropertyName("distributor_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string CompanyPhone { get; set; }
		public string DelivererPhone { get; set; }
	}
}
