using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class MeasurementsController : BaseController<Measurement, MeasurementOutDto, MeasurementInDto>
    {
        public MeasurementsController(ApplicationDbContext dbContext, IMapper mapper, ILogger<MeasurementsController> logger) : base(dbContext, mapper, logger)
        {
        }
    }
}
