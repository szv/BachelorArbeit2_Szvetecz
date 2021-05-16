using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record MeasurementValueOutDto(
        long Id,
        MeasurementOutDto Measurement,
        double Value,
        PositionOutDto Position
    ) : IOutDto<MeasurementValue>;

    public record MeasurementValueInDto(
        long MeasurementId,
        double Value,
        PositionInDto Position
    ) : ICreateDto<MeasurementValue>, IUpdateDto<MeasurementValue>;
}
