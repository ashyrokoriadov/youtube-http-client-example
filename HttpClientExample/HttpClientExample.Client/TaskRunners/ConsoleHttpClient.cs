using HttpClientExample.Client.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExample.Client.TaskRunners
{
    public class ConsoleHttpClient
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }
        public ConsoleHttpClient(ILogger<ConsoleHttpClient> logger, IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public async Task<string> Run(string[] args)
        {
            _logger.LogInformation($"Application started at {DateTime.UtcNow}");

            var endpoint = CreateEndPointAddress(args);
            _logger.LogInformation($"Endpoint is used: {endpoint}");

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var client = _httpFactory.CreateClient();
            var response = await client.SendAsync(request);

            _logger.LogInformation($"Application ended at {DateTime.UtcNow}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Result is {result}");
                return result;
            }
            else
            {                
                return $"StatusCode: {response.StatusCode}";
            }
        }

        private string CreateEndPointAddress(string[] args)
        {
            var baseAddress = "http://localhost:5000";
            switch (args[0])
            {
                case Operations.ADD:
                    return $"{baseAddress}/Calculator/Add?a={args[1]}&b={args[2]}";
                case Operations.SUBTRACT:
                    return $"{baseAddress}/Calculator/Subtract?a={args[1]}&b={args[2]}";
                default:
                    return $"{baseAddress}/Calculator/Add?a={args[1]}&b={args[2]}";
            }
        }
    }
}
