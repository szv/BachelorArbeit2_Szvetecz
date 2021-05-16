namespace DeviceAgent.Database.Entities
{
    public class Actor
    {
        public long Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public long DeviceId { get; set; }

        public Device Device { get; set; }
    }
}
