using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class CompaniesController : BaseController<Company, CompanyOutDto, CompanyInDto>
    {
        public CompaniesController(ApplicationDbContext dbContext, IMapper mapper, ILogger<CompaniesController> logger) : base(dbContext, mapper, logger)
        {
        }
    }
}
