using CountPad.Application.Common.Models;

namespace CountPad.Application.UseCases.Distributors.Models
{
	public class DistributorDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
		public string CompanyPhone { get; set; }
		public string DelivererPhone { get; set; }
	}
}
