using System;
using System.Collections.Generic;
using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record DeviceOutDto(
        long Id,
        string Name,
        string Description,
        int Interval,
        ProjectOutDto Project,
        IEnumerable<ActorOutDto> Actors,
        IEnumerable<MeasurementOutDto> Measurements
    ) : IOutDto<Device>;

    public record DeviceInDto(
        string Name,
        string Description,
        Guid SetupId,
        int Interval,
        IEnumerable<ActorInDto> Actors,
        IEnumerable<MeasurementInDto> Measurements
    ) : ICreateDto<Device>, IUpdateDto<Device>;
}
