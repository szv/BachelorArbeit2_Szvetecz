using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;

namespace Server.Mapping.Profiles
{
    public class ActorMapping : Profile
    {
        public ActorMapping()
        {
            CreateMap<Actor, ActorInDto>();
            CreateMap<ActorOutDto, Actor>();
        }
    }
}
