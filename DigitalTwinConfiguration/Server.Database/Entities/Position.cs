namespace Server.Database.Entities
{
    public class Position : IEntity
    {
        public long Id { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public long MeasurementValueId { get; set; }

        public MeasurementValue MeasurementValue { get; set; }
    }
}
