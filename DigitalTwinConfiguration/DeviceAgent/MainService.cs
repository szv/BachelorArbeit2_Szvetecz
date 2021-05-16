using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeviceAgent.Communication;
using DeviceAgent.Database.Context;
using DeviceAgent.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DeviceAgent
{
    public class MainService : IHostedService
    {
        private readonly IOptions<Options> options;

        private readonly ILogger<MainService> logger;

        private readonly DatabaseContext dbContext;

        private readonly ServerHttpClient httpClient;

        private readonly CancellationTokenSource cancellationTokenSource;

        public MainService(IOptions<Options> options, ILogger<MainService> logger, DatabaseContext dbContext, ServerHttpClient httpClient)
        {
            this.options = options;
            this.logger = logger;
            this.dbContext = dbContext;
            this.httpClient = httpClient;
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Starting...");

            if (this.options.Value.Decentral)
                await this.RegisterDecentralAsync(cancellationToken);
            else
                await this.RegisterCentralAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Stopping...");
            this.cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }

        // for central configuration
        private async Task RegisterCentralAsync(CancellationToken cancellationToken)
        {
            bool loop;
            do
            {
                try
                {
                    this.logger.LogInformation("Registering device...");
                    Device device = await this.httpClient.RegisterAsync(this.options.Value.SetupId, cancellationToken);
                    this.logger.LogInformation("Saving device-data...");
                    await this.dbContext.SetDeviceAsync(device, cancellationToken);
                    loop = false;
                }
                catch (HttpRequestException e)
                {
                    this.logger.LogError(e, "Error registering device. Trying again in 1 minute...");
                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                    loop = true;
                }
                catch (DbUpdateException e)
                {
                    this.logger.LogError(e, "Error saving the device-data.");
                    throw;
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "Error registering device.");
                    throw;
                }
            } while (loop);
        }

        private async Task RegisterDecentralAsync(CancellationToken cancellationToken)
        {
            string json = await File.ReadAllTextAsync("device-configuration.json", cancellationToken);
            Device device = JsonConvert.DeserializeObject<Device>(json);
            bool loop;
            Device savedDevice = null;

            do
            {
                try
                {
                    savedDevice = await this.httpClient.RegisterAsync(this.options.Value.SetupId, device, cancellationToken);
                    loop = false;
                }
                catch (HttpRequestException e)
                {
                    this.logger.LogError(e, "Error registering device. Trying again in 1 minute...");
                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                    loop = true;
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, "Error registering device.");
                    throw;
                }
            } while (loop);

            this.dbContext.Devices.RemoveRange(await this.dbContext.Devices.ToListAsync(cancellationToken));
            this.dbContext.Devices.Add(savedDevice);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
