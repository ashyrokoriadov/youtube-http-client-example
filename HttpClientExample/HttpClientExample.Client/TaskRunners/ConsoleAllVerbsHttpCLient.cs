using HttpClientExample.Library;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientExample.Client.TaskRunners
{
    class ConsoleAllVerbsHttpCLient
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }
        public ConsoleAllVerbsHttpCLient(ILogger<ConsoleHttpClient> logger, IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public async Task<string> Run(string[] args)
        {
            _logger.LogInformation($"Application started at {DateTime.UtcNow}");

            var endpoint = "http://localhost:5000/Calculator";
            var requestBody = new CalculatorRequest(10.0, 20.0);
            var requestContent  = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var client = _httpFactory.CreateClient();

            var postResponse = await client.PostAsync(endpoint, requestContent);
            if (postResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(await postResponse.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine($"StatusCode: {postResponse.StatusCode}");
            }

            var putResponse = await client.PutAsync(endpoint, requestContent);
            if (putResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(await putResponse.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine($"StatusCode: {putResponse.StatusCode}");
            }

            var deleteResponse = await client.DeleteAsync(endpoint);
            if (deleteResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(await deleteResponse.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine($"StatusCode: {deleteResponse.StatusCode}");
            }

            _logger.LogInformation($"Application ended at {DateTime.UtcNow}");
            return "OK";
        }
    }
}
