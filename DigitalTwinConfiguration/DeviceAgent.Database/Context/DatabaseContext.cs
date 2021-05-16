using System;
using System.Threading;
using System.Threading.Tasks;
using DeviceAgent.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceAgent.Database.Context
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets the actors.
        /// </summary>
        /// <value>
        /// The actors.
        /// </value>
        public DbSet<Actor> Actors { get; private set; }

        /// <summary>
        /// Gets the measurements.
        /// </summary>
        /// <value>
        /// The measurements.
        /// </value>
        public DbSet<Measurement> Measurements { get; private set; }

        /// <summary>
        /// Gets the measurement values.
        /// </summary>
        /// <value>
        /// The measurement values.
        /// </value>
        public DbSet<MeasurementValue> MeasurementValues { get; private set; }

        /// <summary>
        /// Gets the positions.
        /// </summary>
        /// <value>
        /// The positions.
        /// </value>
        public DbSet<Position> Positions { get; private set; }

        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets the device asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The device or null, if no device exists.</returns>
        public Task<Device> GetDeviceAsync(CancellationToken cancellationToken) => this.Devices.SingleOrDefaultAsync(cancellationToken);

        /// <summary>
        /// Sets/Saves the device asynchronously.
        /// </summary>
        /// <param name="device">The device to set/save.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public async Task SetDeviceAsync(Device device, CancellationToken cancellationToken)
        {
            if (await this.Devices.AnyAsync())
                throw new ArgumentOutOfRangeException("There is already a device stored. Only one device is allowed here.");

            this.Devices.Add(device);
            await this.SaveChangesAsync(cancellationToken);
        }
    }
}
