namespace DeviceAgent.Database.Entities
{
    public class MeasurementValue
    {
        public long Id { get; init; }

        public long MeasurementId { get; set; }

        public Measurement Measurement { get; set; }

        public double Value { get; set; }

        public Position Position { get; set; }
    }
}
