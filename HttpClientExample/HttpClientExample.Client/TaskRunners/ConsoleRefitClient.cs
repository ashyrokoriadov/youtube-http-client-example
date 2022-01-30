using HttpClientExample.Client.Clients;
using HttpClientExample.Client.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientExample.Client.TaskRunners
{
    class ConsoleRefitClient
    {
        private readonly ILogger _logger;
        private readonly ICalculatorHttpClient _httpClient;

        public ConsoleRefitClient(ILogger<ConsoleHttpNamedClient> logger, ICalculatorHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<string> Run(string[] args)
        {
            _logger.LogInformation($"Application started at {DateTime.UtcNow}");

            var result = 0.0;

            switch (args[0])
            {
                case Operations.ADD:
                    result = await _httpClient.Add(double.Parse(args[1]), double.Parse(args[2]));
                    break;
                case Operations.SUBTRACT:
                    result = await _httpClient.Subtract(double.Parse(args[1]), double.Parse(args[2]));
                    break;
                default:
                    result = await _httpClient.Add(double.Parse(args[1]), double.Parse(args[2]));
                    break;
            }

            _logger.LogInformation($"Application ended at {DateTime.UtcNow}");

            return result.ToString();
        }
    }
}
