using Refit;
using System.Threading.Tasks;

namespace HttpClientExample.Client.Clients
{
    interface ICalculatorHttpClient
    {
        [Get("/Calculator/Add?a={a}&b={b}")]
        Task<double> Add(double a, double b);

        [Get("/Calculator/Subtract?a={a}&b={b}")]
        Task<double> Subtract(double a, double b);
    }
}
