using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.UseCases.Products.Models;
using MediatR;

namespace CountPad.Application.UseCases.Products.Commands.CreateProduct
{
	public class CreateProductCommand : IRequest<ProductDto>
	{

	}
}
