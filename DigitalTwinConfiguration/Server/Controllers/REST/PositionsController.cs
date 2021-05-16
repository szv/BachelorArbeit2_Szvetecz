using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class PositionsController : BaseController<Position, PositionOutDto, PositionInDto>
    {
        public PositionsController(ApplicationDbContext dbContext, IMapper mapper, ILogger<Position> logger) : base(dbContext, mapper, logger)
        {
        }
    }
}
