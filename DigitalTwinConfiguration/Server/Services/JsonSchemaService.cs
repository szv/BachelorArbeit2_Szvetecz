using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Json.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Server.Services
{
    public class JsonSchemaService
    {
        private ConcurrentDictionary<string, JsonSchema> schemas;

        public JsonSchemaService(ILogger<JsonSchemaService> logger, IOptions<Options> options)
        {
            this.schemas = new ConcurrentDictionary<string, JsonSchema>();

            string[] filePaths = Directory.GetFiles(options.Value.SchemaPath);

            foreach (string filePath in filePaths)
            {
                if (!filePath.EndsWith(".schema.json"))
                    continue;

                JsonSchema schema = JsonSchema.FromFile(filePath);
                IdKeyword id = schema.Keywords.Where(x => x.Keyword() == "$id").FirstOrDefault() as IdKeyword;
                string name = id.Id.Segments[id.Id.Segments.Length - 1].Split('.')[0];
                SchemaRegistry.Global.Register(new Uri(id.Id.AbsoluteUri), schema);

                if (!this.schemas.TryAdd(name, schema))
                    logger.LogWarning($"Error adding schema \"{name}\"");
            }
        }

        public IReadOnlyDictionary<string, JsonSchema> Schemas => this.schemas;
    }
}
