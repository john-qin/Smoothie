using System;
using AutoMapper;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Entities;
using Smoothie.Domain.Enums;
using Smoothie.Domain.ViewModels;

namespace Smoothie.Web.Infrastructure.AutoMapper
{
    public class ViewModelProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModel"; }
        }

        protected override void Configure()
        {
            // specify teh mapping configurations here

            CreateMap<UserLoginViewModel, User>()
                .ForMember(dest => dest.CreatedDate, t => t.MapFrom(s => DateTime.Now))
                .ForMember(dest => dest.DisplayName, t => t.MapFrom(s => ""));

            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.Firstname, t => t.MapFrom(s => ""))
                .ForMember(dest => dest.Lastname, t => t.MapFrom(s => ""))
                .ForMember(dest => dest.CreatedDate, t => t.MapFrom(s => DateTime.Now))
                .ForMember(dest => dest.LastLogin, t => t.MapFrom(s => DateTime.Now))
                .ForMember(dest => dest.AccountType, t => t.MapFrom(s => AccountType.Smoothie))
                .ForMember(dest => dest.Roles, t => t.MapFrom(s => RoleType.Member))

                .ForMember(dest => dest.Avatar, t => t.MapFrom(s => ""))
                .ForMember(dest => dest.ThirdPartyId, t => t.MapFrom(s => ""))
                .ForMember(dest => dest.Status, t => t.MapFrom(s => Status.Approved))
                .ForMember(dest => dest.Ip, t => t.MapFrom(s => ""));

            CreateMap<User, UserDataDto>();


        }
    }
}