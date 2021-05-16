using Server.Database.Entities;

namespace Exchange.Dtos
{
    public interface ICreateDto<TEntity>
        where TEntity : class, IEntity
    {
    }
}
