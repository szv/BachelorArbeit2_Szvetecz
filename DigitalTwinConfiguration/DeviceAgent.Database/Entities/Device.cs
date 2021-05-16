using System;
using System.Collections.Generic;

namespace DeviceAgent.Database.Entities
{
    public class Device
    {
        public long Id { get; init; }

        public Guid SetupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Interval { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Measurement> Measurements { get; set; }
    }
}
