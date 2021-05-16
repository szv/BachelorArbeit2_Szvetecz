using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class ActorsController : BaseController<Actor, ActorOutDto, ActorInDto>
    {
        public ActorsController(ApplicationDbContext dbContext, IMapper mapper, ILogger<ActorsController> logger) : base(dbContext, mapper, logger)
        {
        }
    }
}
