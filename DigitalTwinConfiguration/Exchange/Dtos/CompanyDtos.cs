using Server.Database.Entities;

namespace Exchange.Dtos
{
    public record CompanyOutDto(
        long Id,
        string Name,
        string EMail,
        ProjectOutDto Project
    ) : IOutDto<Company>;

    public record CompanyInDto(
        string Name,
        string EMail
    ) : ICreateDto<Company>, IUpdateDto<Company>;
}
