using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;

namespace Server.Mapping.Profiles
{
    public class DeviceMapping : Profile
    {
        public DeviceMapping()
        {
            CreateMap<Device, DeviceInDto>();
            CreateMap<DeviceOutDto, Device>();
        }
    }
}
