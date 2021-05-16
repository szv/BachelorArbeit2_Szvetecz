using System.Collections.Generic;
using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record MeasurementOutDto(
        long Id,
        string Name,
        string Description,
        string Unit,
        int Interval,
        IEnumerable<MeasurementValueOutDto> MeasurementValues
    ) : IOutDto<Measurement>;

    public record MeasurementInDto(
        string Name,
        string Description,
        string Unit,
        int Interval
    ) : ICreateDto<Measurement>, IUpdateDto<Measurement>;
}
