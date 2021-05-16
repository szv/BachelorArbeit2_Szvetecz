using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class PositionMapping : Profile
    {
        public PositionMapping()
        {
            CreateMap<Position, PositionOutDto>();
            CreateMap<PositionInDto, Position>();
        }
    }
}
