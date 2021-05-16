using System;
using System.Collections.Generic;
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
    public class BaseController<TEntity> : BaseController<TEntity, TEntity, TEntity, TEntity, long>
        where TEntity : class, IEntity, IOutDto<TEntity>, ICreateDto<TEntity>, IUpdateDto<TEntity>, new()
    {
        public BaseController(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) { }
    }

    public class BaseController<TEntity, TDto> : BaseController<TEntity, TDto, TDto, TDto, long>
        where TEntity : class, IEntity, new()
        where TDto : class, IOutDto<TEntity>, ICreateDto<TEntity>, IUpdateDto<TEntity>
    {
        public BaseController(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) { }
    }

    public class BaseController<TEntity, TOutDto, TInDto> : BaseController<TEntity, TOutDto, TInDto, TInDto, long>
        where TEntity : class, IEntity, new()
        where TOutDto : class, IOutDto<TEntity>
        where TInDto : class, ICreateDto<TEntity>, IUpdateDto<TEntity>
    {
        public BaseController(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TOutDto, TCreateDto, TUpdateDto, TId> : ControllerBase
        where TEntity : class, IEntity, new()
        where TOutDto : class, IOutDto<TEntity>
        where TCreateDto : class, ICreateDto<TEntity>
        where TUpdateDto : class, IUpdateDto<TEntity>
        where TId : IEquatable<TId>
    {
        private readonly ApplicationDbContext dbContext;

        private readonly IMapper mapper;

        private readonly ILogger logger;

        public BaseController(ApplicationDbContext dbContext, IMapper mapper, ILogger logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        protected ApplicationDbContext DbContext => this.dbContext;

        protected DbSet<TEntity> Entities => this.dbContext.Set<TEntity>();

        protected IMapper Mapper => this.mapper;

        protected ILogger Logger => this.logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TOutDto>>> Get(CancellationToken cancellationToken)
        {
            List<TEntity> entities = await this.Entities.ToListAsync(cancellationToken);
            IEnumerable<TOutDto> dtos = this.Mapper.Map<IEnumerable<TEntity>, IEnumerable<TOutDto>>(entities);
            return this.Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TOutDto>> Get(TId id, CancellationToken cancellationToken)
        {
            TEntity entity = await this.Entities
                .AsNoTracking()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
                return this.NotFound();

            return this.Mapper.Map<TEntity, TOutDto>(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TId id, [FromBody] TUpdateDto input, CancellationToken cancellationToken)
        {
            TEntity entity = await this.Entities
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
                return this.NotFound();

            this.Mapper.Map<TUpdateDto, TEntity>(input, entity);

            try
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message, e);
                return this.BadRequest();
            }

            return this.NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TCreateDto input, CancellationToken cancellationToken)
        {
            TEntity entity = this.Mapper.Map<TCreateDto, TEntity>(input);
            this.Entities.Add(entity);

            try
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message, e);
                return this.BadRequest();
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TId id, CancellationToken cancellationToken)
        {
            TEntity entity = await this.Entities.AsNoTracking().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
            this.DbContext.Set<TEntity>().Remove(entity);

            try
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message, e);
                return this.BadRequest();
            }

            return this.NoContent();
        }
    }
}
