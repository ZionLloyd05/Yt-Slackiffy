using AutoMapper;
using Slackiffy.Models;

namespace Slackiffy.Utilities
{
    public class SlackiffyProfile : Profile
    {
        public SlackiffyProfile()
        {
            CreateMap<Message, Models.DTOs.MessagePack>()
                .ForMember(msgPck => msgPck.Message, opt =>
                    opt.MapFrom(msg => msg.Chat))
                .ForMember(msgPck => msgPck.UserName, opt =>
                    opt.MapFrom(msg => msg.FromUser.Username))
                .ForMember(msgPck => msgPck.Picture, opt =>
                    opt.MapFrom(msg => msg.FromUser.Picture));
        }
    }
}
