using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class ActorMapping : Profile
    {
        public ActorMapping()
        {
            CreateMap<Actor, ActorOutDto>();
            CreateMap<ActorInDto, Actor>();
        }
    }
}
