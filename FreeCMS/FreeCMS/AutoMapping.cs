using AutoMapper;
using FreeCMS.Areas.Core.ViewModels;
using FreeCMS.DomainModels.System;

namespace FreeCMS
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<NewSocialNetwork, SocialNetwork>();
            CreateMap<SocialNetwork, EditableSocialNetwork>().ReverseMap();
        }
    }
}