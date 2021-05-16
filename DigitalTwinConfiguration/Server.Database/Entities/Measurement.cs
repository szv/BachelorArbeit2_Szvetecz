using System.Collections.Generic;

namespace Server.Database.Entities
{
    public class Measurement : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public int Interval { get; set; }

        public long DeviceId { get; set; }

        public Device Device { get; set; }

        public ICollection<MeasurementValue> MeasurementValues { get; set; }
    }
}
