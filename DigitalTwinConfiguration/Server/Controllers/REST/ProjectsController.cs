using AutoMapper;
using Exchange.Dtos;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class ProjectsController : BaseController<Project, ProjectOutDto, ProjectInDto>
    {
        public ProjectsController(ApplicationDbContext dbContext, IMapper mapper, ILogger<ProjectsController> logger)
            : base(dbContext, mapper, logger)
        {
        }
    }
}
