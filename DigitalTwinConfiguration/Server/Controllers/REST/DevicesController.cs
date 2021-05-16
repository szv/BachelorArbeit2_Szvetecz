using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exchange.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Database.Context;
using Server.Database.Entities;

namespace Server.Controllers.REST
{
    public class DevicesController : BaseController<Device, DeviceOutDto, DeviceInDto>
    {
        public DevicesController(ApplicationDbContext dbContext, IMapper mapper, ILogger<DevicesController> logger) : base(dbContext, mapper, logger)
        {
        }

        [HttpGet("{setupId}/register")]
        public async Task<ActionResult<DeviceOutDto>> Register(Guid setupId, CancellationToken cancellationtoken)
        {
            Device device = await this.Entities
                .Include(x => x.Actors)
                .Include(x => x.Measurements)
                .Where(x => x.SetupId == setupId)
                .FirstOrDefaultAsync(cancellationtoken);

            if (device == null)
                return this.NotFound();

            return this.Mapper.Map<Device, DeviceOutDto>(device);
        }

        [HttpPut("{setupId}/register")]
        public async Task<ActionResult<DeviceOutDto>> Register(Guid setupId, DeviceInDto input, CancellationToken cancellationtoken)
        {
            Device device = await this.Entities
                .Where(x => x.SetupId == setupId)
                .FirstOrDefaultAsync(cancellationtoken);

            if (device == null)
                return this.NotFound();

            this.Mapper.Map(input, device);

            try
            {
                await this.DbContext.SaveChangesAsync(cancellationtoken);
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message, e);
                return this.BadRequest();
            }

            Device savedDevice = await this.Entities
                .Where(x => x.SetupId == setupId)
                .FirstOrDefaultAsync(cancellationtoken);

            return this.Mapper.Map<DeviceOutDto>(savedDevice);
        }
    }
}
