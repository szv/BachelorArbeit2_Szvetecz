using System;
using System.Collections.Generic;

namespace Server.Database.Entities
{
    public class Device : IEntity
    {
        public long Id { get; set; }

        public Guid SetupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Interval { get; set; }

        public long ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Measurement> Measurements { get; set; }
    }
}
