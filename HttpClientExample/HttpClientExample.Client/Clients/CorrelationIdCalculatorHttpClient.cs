using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExample.Client.Clients
{
    class CorrelationIdCalculatorHttpClient : ICalculatorHttpClient
    {
        private readonly HttpClient _httpClient;

        public CorrelationIdCalculatorHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
            _httpClient.DefaultRequestHeaders.Add("CorrelationId", Guid.NewGuid().ToString());
        }

        public async Task<double> Add(double a, double b)
        {
            var endpoint = $"Calculator/Add?a={a}&b={b}";
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = double.Parse(content);
                return result;
            }

            return 0.0;
        }

        public async Task<double> Subtract(double a, double b)
        {
            var endpoint = $"Calculator/Subtract?a={a}&b={b}";
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = double.Parse(content);
                return result;
            }

            return 0.0;
        }
    }
}
