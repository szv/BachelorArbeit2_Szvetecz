using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Server.Database.Context;
using Server.Mapping.Profiles;
using Server.Middleware;
using Server.Services;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<Options>()
                .Bind(this.Configuration)
                .ValidateDataAnnotations();

            services.AddLogging(x =>
                x.AddConsole()
                .SetMinimumLevel(LogLevel.Information)
                .AddEventLog());

            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql(this.Configuration.GetConnectionString("Database"));
                x.UseSnakeCaseNamingConvention();
            });
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1", Contact = new OpenApiContact { Name = "Sebastian Szvetecz" } }));
            services.AddAutoMapper(typeof(ProjectMapping).Assembly);

            services.AddSingleton<JsonSchemaService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ValidationMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
