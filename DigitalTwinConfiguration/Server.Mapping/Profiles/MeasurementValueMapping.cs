using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class MeasurementValueMapping : Profile
    {
        public MeasurementValueMapping()
        {
            CreateMap<MeasurementValue, MeasurementValueOutDto>();
            CreateMap<MeasurementValueInDto, MeasurementValue>();
        }
    }
}
