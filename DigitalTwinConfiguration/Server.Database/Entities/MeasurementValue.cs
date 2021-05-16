namespace Server.Database.Entities
{
    public class MeasurementValue : IEntity
    {
        public long Id { get; set; }

        public long MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public double Value { get; set; }

        public Position Position { get; set; }
    }
}
