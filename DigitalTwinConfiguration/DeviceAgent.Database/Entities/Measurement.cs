namespace DeviceAgent.Database.Entities
{
    public class Measurement
    {
        public long Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public int Interval { get; set; }

        public long DeviceId { get; set; }

        public Device Device { get; set; }
    }
}
