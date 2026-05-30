using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Domain.Auth.Entities;
using AutoMapper;

namespace AuthPlatform.Application.Auth.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<AuthUser, AuthUserDto>().ReverseMap();

            CreateMap<AuthGroup, AuthGroupDto>().ReverseMap();

            CreateMap<AuthPermission, AuthPermissionDto>().ReverseMap();

            CreateMap<AuthUser, AuthUserDto>().ReverseMap();

            CreateMap<AuthUser, AuthUserListDto>().ReverseMap();
            CreateMap<AuthUserPermission, AuthUserPermissionDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.AuthUser.UserName))
                .ForMember(dest => dest.PermissionName,
                    opt => opt.MapFrom(src => src.AuthPermission.PermissionName))
                .ForMember(dest => dest.PermissionCode,
                    opt => opt.MapFrom(src => src.AuthPermission.PermissionCode))
                .ReverseMap();

            CreateMap<AuthGroupPermission, AuthGroupPermissionDto>()
                .ForMember(dest => dest.GroupName,
                    opt => opt.MapFrom(src => src.AuthGroup.GroupName))
                .ForMember(dest => dest.PermissionName,
                    opt => opt.MapFrom(src => src.AuthPermission.PermissionName))
                .ForMember(dest => dest.PermissionCode,
                    opt => opt.MapFrom(src => src.AuthPermission.PermissionCode))
                .ReverseMap();

            CreateMap<AuthUserGroup, AuthUserGroupDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.AuthUser.UserName))
                .ForMember(dest => dest.GroupName,
                    opt => opt.MapFrom(src => src.AuthGroup.GroupName))
                .ReverseMap();

        }
    }
}