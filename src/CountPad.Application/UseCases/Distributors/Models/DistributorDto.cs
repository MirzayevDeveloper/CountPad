using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.Common.Models;
using CountPad.Domain.Common.BaseEntities;

namespace CountPad.Application.UseCases.Distributors.Models
{
    public class DistributorDto : BaseAuditableEntityDto
    {
        public string Name { get; set; }
        public string CompanyPhone { get; set; }
        public string DelivererPhone { get; set; }
    }
}
