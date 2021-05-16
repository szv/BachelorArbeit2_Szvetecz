namespace Server.Database.Entities
{
    public class Company : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public long ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
