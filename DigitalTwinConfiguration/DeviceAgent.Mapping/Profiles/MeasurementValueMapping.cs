using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;

namespace Server.Mapping.Profiles
{
    public class MeasurementValueMapping : Profile
    {
        public MeasurementValueMapping()
        {
            CreateMap<MeasurementValue, MeasurementValueInDto>();
            CreateMap<MeasurementValueOutDto, MeasurementValue>();
        }
    }
}
