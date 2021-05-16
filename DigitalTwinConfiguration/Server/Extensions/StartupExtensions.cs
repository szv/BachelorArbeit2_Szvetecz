using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Database.Context;

namespace Server.Extensions
{
    public static class StartupExtensions
    {
        public static IHost CreateDbIfNotExists(this IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                try
                {
                    serviceProvider.GetRequiredService<ApplicationDbContext>().Initialize();
                }
                catch (Exception e)
                {
                    serviceProvider.GetRequiredService<ILogger<Program>>().LogError(e, "An error occured, while initializing the database.");
                }
            }

            return host;
        }
    }
}
