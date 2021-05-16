using Server.Database.Entities;

namespace Exchange.Dtos
{
    public interface IOutDto<TEntity>
        where TEntity : class, IEntity
    {
        long Id { get; init; }
    }
}
