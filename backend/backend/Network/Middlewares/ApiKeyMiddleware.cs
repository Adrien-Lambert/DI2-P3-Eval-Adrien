using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Network.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyMiddleware> _logger;
        private const string API_KEY_HEADER_NAME = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
            {
                _logger.LogWarning("API Key missing");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            var validApiKeys = _configuration.GetSection("ApiKeys").Get<string[]>().ToList();

            if (validApiKeys == null || !validApiKeys.Contains(extractedApiKey))
            {
                _logger.LogWarning("Invalid API Key");
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}
