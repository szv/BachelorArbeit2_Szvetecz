using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record PositionOutDto(
        long Id,
        double Lat,
        double Lon,
        MeasurementValueOutDto MeasurementValue
    ) : IOutDto<Position>;

    public record PositionInDto(
        double Lat,
        double Lon
    ) : ICreateDto<Position>, IUpdateDto<Position>;
}
