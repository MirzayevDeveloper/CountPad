using AutoMapper;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Roles;

namespace CountPad.Application.Common.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
        }
    }
}
