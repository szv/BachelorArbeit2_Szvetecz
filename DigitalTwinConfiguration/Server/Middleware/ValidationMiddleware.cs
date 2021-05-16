using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Json.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Server.Services;

namespace Server.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate next;

        private readonly JsonSchemaService jsonSchemaService;

        private readonly ILogger<ValidationMiddleware> logger;

        public ValidationMiddleware(RequestDelegate next, JsonSchemaService jsonSchemaService, ILogger<ValidationMiddleware> logger)
        {
            this.next = next;
            this.jsonSchemaService = jsonSchemaService;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!this.IsUploadMethod(httpContext.Request.Method))
            {
                await this.next(httpContext);
                return;
            }

            if (!httpContext.Request.Path.HasValue)
            {
                await this.next(httpContext);
                return;
            }

            string path = httpContext.Request.Path.Value;

            if (!path.StartsWith("/api/"))
            {
                await this.next(httpContext);
                return;
            }

            string[] pathParts = path.Split('/');

            if (pathParts.Length < 3 || string.IsNullOrWhiteSpace(pathParts[2]))
            {
                await this.next(httpContext);
                return;
            }

            string type = pathParts[2];

            if (type[^1] == 's')
                type = type.Remove(type.Length - 1);

            if (!this.jsonSchemaService.Schemas.TryGetValue(type, out JsonSchema schema))
            {
                this.logger.LogDebug($"No schema registered for \"{type}\"");
                await this.next(httpContext);
                return;
            }

            JsonDocument json;

            try
            {
                httpContext.Request.EnableBuffering();
                using StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true);
                json = JsonDocument.Parse(await reader.ReadToEndAsync());
                httpContext.Request.Body.Position = 0;
            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.StartAsync();
                return;
            }
            
            ValidationResults results = schema.Validate(json.RootElement);

            if (!results.IsValid)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.StartAsync();
                return;
            }

             await this.next(httpContext);
        }

        private bool IsUploadMethod(string method)
        {
            method = method.ToLower();
            return method == "post"
                || method == "put"
                || method == "patch";
        }
    }
}
