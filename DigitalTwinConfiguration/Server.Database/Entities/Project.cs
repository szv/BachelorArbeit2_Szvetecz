using System.Collections.Generic;

namespace Server.Database.Entities
{
    public class Project : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Company Company { get; set; }

        public ICollection<Device> Devices { get; set; }
    }
}
