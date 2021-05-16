using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class MeasurementMapping : Profile
    {
        public MeasurementMapping()
        {
            CreateMap<Measurement, MeasurementOutDto>();
            CreateMap<MeasurementInDto, Measurement>();
        }
    }
}
