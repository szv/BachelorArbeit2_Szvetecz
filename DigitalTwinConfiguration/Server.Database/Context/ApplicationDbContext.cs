using Server.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Database.ContextConfiguration;

namespace Server.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; private set; }

        public DbSet<Company> Companies { get; private set; }

        public DbSet<Device> Devices { get; private set; }

        public DbSet<Actor> Actors { get; private set; }

        public DbSet<Measurement> Measurements { get; private set; }

        public DbSet<MeasurementValue> MeasurementValues { get; private set; }

        public DbSet<Position> Positions { get; private set; }

        public void Initialize() => this.Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }
    }
}
