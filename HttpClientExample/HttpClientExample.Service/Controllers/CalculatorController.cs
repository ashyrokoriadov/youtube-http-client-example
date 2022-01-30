using HttpClientExample.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientExample.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {   
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Add")]
        public double Add(double a, double b)
        {
            var result = a + b;
            _logger.LogInformation($"Calculated {a} + {b} = {result}");
            return result;
        }

        [HttpGet("Subtract")]
        public double Subtract(double a, double b)
        {
            var result = a - b;
            _logger.LogInformation($"Calculated {a} - {b} = {result}");
            return result;
        }

        [HttpPost]
        public string PostRequest(CalculatorRequest request)
        {
            return $"POST request: A = {request.A}; B = {request.B}.";
        }

        [HttpPut]
        public string PutRequest(CalculatorRequest request)
        {
            return $"PUT request: A = {request.A}; B = {request.B}.";
        }

        [HttpDelete]
        public string DeleteRequest()
        {
            return $"DELETE request.";
        }
    }
}
