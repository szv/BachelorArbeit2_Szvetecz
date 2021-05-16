using System;

namespace DeviceAgent
{
    public class Options
    {
        public Uri ApiEndpoint { get; init; }

        public Guid SetupId { get; init; }

        public bool Decentral { get; init; }
    }
}
