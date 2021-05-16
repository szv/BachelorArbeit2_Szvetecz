using System.ComponentModel.DataAnnotations;

namespace Server
{
    public class Options
    {
        [Required]
        public string SchemaPath { get; init; }
    }
}
