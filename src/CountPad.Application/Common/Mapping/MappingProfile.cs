using AutoMapper;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Users;

namespace CountPad.Application.Common.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Role, RoleDto>().ReverseMap();
			CreateMap<UserDto, User>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Package, PackageDto>().ReverseMap();
			CreateMap<Permission, PermissionDto>().ReverseMap();
			CreateMap<DistributorDto, Distributor>().ReverseMap();
			CreateMap<ProductCategoryDto, ProductCategory>().ReverseMap();
		}
	}
}
