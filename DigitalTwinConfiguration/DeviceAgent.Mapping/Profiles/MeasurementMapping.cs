using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;

namespace Server.Mapping.Profiles
{
    public class MeasurementMapping : Profile
    {
        public MeasurementMapping()
        {
            CreateMap<Measurement, MeasurementInDto>();
            CreateMap<MeasurementOutDto, Measurement>();
        }
    }
}
