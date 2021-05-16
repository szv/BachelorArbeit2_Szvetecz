using System;
using DeviceAgent.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeviceAgent.Extensions
{
    public static class HostExtensions
    {
        public static IHost CreateDbIfNotExists(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    serviceProvider.GetRequiredService<DatabaseContext>().Database.EnsureCreated();
                }
                catch (Exception e)
                {
                    serviceProvider.GetRequiredService<ILogger<Program>>().LogError(e, "An error occured, while initializing the database.");
                }
            }

            return host;
        }

        public static IHostBuilder UseCurrentEnvironment(this IHostBuilder host)
        {
#if DEBUG
            return host.UseEnvironment(Environments.Development);
#else
            return host.UseEnvironment(Environments.Production);
#endif
        }
    }
}
