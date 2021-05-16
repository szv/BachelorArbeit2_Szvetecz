using System.IO;
using System.Threading.Tasks;
using DeviceAgent.Communication;
using DeviceAgent.Database.Context;
using DeviceAgent.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Mapping.Profiles;

namespace DeviceAgent
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using IHost host = new HostBuilder()
                .UseCurrentEnvironment()
                .ConfigureAppConfiguration((hostContext, appConfig) =>
                {
                    appConfig.SetBasePath(Directory.GetCurrentDirectory());
                    appConfig.AddJsonFile("appsettings.json");
                    appConfig.AddCommandLine(args);
                    appConfig.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<Options>()
                        .Bind(hostContext.Configuration);
                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        options.UseSqlite(hostContext.Configuration.GetConnectionString("Database"));
                    });
                    services.AddAutoMapper(typeof(DeviceMapping).Assembly);
                    services.AddScoped<ServerHttpClient>();
                    services.AddHostedService<MainService>();
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.AddConsole();

                    if (hostContext.HostingEnvironment.IsDevelopment())
                        logging.SetMinimumLevel(LogLevel.Debug);
                    else
                        logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseConsoleLifetime()
                .Build();

            try
            {
                host
                .CreateDbIfNotExists()
                .Start();
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }
}
