using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class MeasurementValuesController : BaseController<MeasurementValue, MeasurementValueOutDto, MeasurementValueInDto>
    {
        public MeasurementValuesController(ApplicationDbContext dbContext, IMapper mapper, ILogger<MeasurementValue> logger) : base(dbContext, mapper, logger)
        {
        }
    }
}
