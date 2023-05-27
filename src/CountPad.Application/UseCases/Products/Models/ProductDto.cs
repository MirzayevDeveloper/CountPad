using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.Common.Models;
using CountPad.Domain.Entities.Products;

namespace CountPad.Application.UseCases.Products.Models
{
	public class ProductDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }

		public Guid ProductCategoryId { get; set; }
		public ProductCategory ProductCategory { get; set; }

		public string Description { get; set; }
	}
}
