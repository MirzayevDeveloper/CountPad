using AutoMapper;
using CountPad.Application.UseCases.Permissions.Commands.UpdatePermission;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;

namespace CountPad.Application.Common.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<UpdatePermissionCommand, PermissionDto>().ReverseMap();
        }
    }
}
