using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record ActorOutDto(
        long Id,
        string Name,
        string Description,
        string Type
    ) : IOutDto<Actor>;

    public record ActorInDto(
        string Name,
        string Description,
        string Type
    ) : ICreateDto<Actor>, IUpdateDto<Actor>;
}
