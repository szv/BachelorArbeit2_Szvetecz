using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class DeviceMapping : Profile
    {
        public DeviceMapping()
        {
            CreateMap<Device, DeviceOutDto>();
            CreateMap<DeviceInDto, Device>();
        }
    }
}
