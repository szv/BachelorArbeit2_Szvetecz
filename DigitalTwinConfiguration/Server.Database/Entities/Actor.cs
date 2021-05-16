namespace Server.Database.Entities
{
    public class Actor : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public long DeviceId { get; set; }

        public Device Device { get; set; }
    }
}
