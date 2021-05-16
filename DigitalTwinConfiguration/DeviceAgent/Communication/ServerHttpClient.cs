using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeviceAgent.Database.Entities;
using Exchange.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DeviceAgent.Communication
{
    public class ServerHttpClient
    {
        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHttpClient"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="mapper">The mapper.</param>
        public ServerHttpClient(IOptions<Options> options, IMapper mapper)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = options.Value.ApiEndpoint;
            this.httpClient.Timeout = TimeSpan.FromMinutes(1);
            this.mapper = mapper;
        }

        /// <summary>
        /// Registration for centralized configuration
        /// It downloads the configuration from the server, according to the matching setupId       
        /// </summary>
        /// <param name="setupId">The setup identifier of this device.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The device-configuration on the server.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async Task<Device> RegisterAsync(Guid setupId, CancellationToken cancellationToken)
        {
            string registerPath = Path.Combine("devices", setupId.ToString(), "register");
            HttpResponseMessage result = await this.httpClient.GetAsync(registerPath, cancellationToken);
            result.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Device>(await result.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Registration for decentralized configuration
        /// It uploads its own configuration to the server, if the an (empty) device with the same setupId exists.   
        /// </summary>
        /// <param name="setupId">The setup identifier of this device.</param>
        /// <param name="configuration">The device-configuration to register.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The device-configuration saved at the server.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async Task<Device> RegisterAsync(Guid setupId, Device configuration, CancellationToken cancellationToken)
        {
            DeviceInDto dto = this.mapper.Map<DeviceInDto>(configuration);
            string registerPath = Path.Combine("devices", setupId.ToString(), "register");
            HttpResponseMessage result = await this.httpClient.PutAsync(registerPath, this.ConvertToContent(dto), cancellationToken);
            result.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Device>(await result.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Converts to the data to StringContent as serialized JSON.
        /// </summary>
        /// <remarks>
        /// Necessary to serialize with camelcasing for Json-Schema validation on the server.
        /// </remarks>
        /// <typeparam name="T">The type of the data to send.</typeparam>
        /// <param name="data">The data to be sent.</param>
        /// <returns>The data as StringContent serialized as JSON.</returns>
        private StringContent ConvertToContent<T>(T data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return new StringContent(JsonConvert.SerializeObject(data, settings), Encoding.UTF8, "application/json");
        }
    }
}
