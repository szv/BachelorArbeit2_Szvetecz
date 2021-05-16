namespace DeviceAgent.Database.Entities
{
    public class Position
    {
        public long Id { get; init; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public long MeasurementValueId { get; set; }

        public MeasurementValue MeasurementValue { get; set; }
    }
}
