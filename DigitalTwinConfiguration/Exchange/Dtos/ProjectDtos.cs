using System.Collections.Generic;
using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record ProjectOutDto(
        long Id,
        string Name,
        string Description,
        CompanyOutDto Company,
        IEnumerable<DeviceOutDto> Devices
    ) : IOutDto<Project>;

    public record ProjectInDto(
        string Name,
        string Description,
        CompanyInDto Company,
        IEnumerable<DeviceInDto> Devices
    ) : ICreateDto<Project>, IUpdateDto<Project>;
}
