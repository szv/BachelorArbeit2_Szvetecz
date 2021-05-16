using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;

namespace Server.Mapping.Profiles
{
    public class PositionMapping : Profile
    {
        public PositionMapping()
        {
            CreateMap<Position, PositionInDto>();
            CreateMap<PositionOutDto, Position>();
        }
    }
}
