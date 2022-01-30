using HttpClientExample.Client.Clients;
using HttpClientExample.Client.Constants;
using HttpClientExample.Client.TaskRunners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Threading.Tasks;

namespace HttpClientExample.Client
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();

                    services.AddHttpClient(ClientNames.SIMPLE_CLIENT, httpClient =>
                    {
                        httpClient.DefaultRequestHeaders.Add("ClientName", ClientNames.SIMPLE_CLIENT);
                    });

                    services.AddHttpClient(ClientNames.CORRELATION_ID_CLIENT, httpClient =>
                    {
                        httpClient.DefaultRequestHeaders.Add("ClientName", ClientNames.CORRELATION_ID_CLIENT);
                        httpClient.DefaultRequestHeaders.Add("CorrelationId", Guid.NewGuid().ToString());
                    });

                    services.AddHttpClient<SimpleHttpClient>();
                    services.AddHttpClient<CorrelationIdCalculatorHttpClient>();

                    services.AddHttpClient(ClientNames.REFIT_CLIENT, httpClient =>
                    {
                        httpClient.BaseAddress = new Uri("http://localhost:5000");

                        httpClient.DefaultRequestHeaders.Add("ClientName", ClientNames.REFIT_CLIENT);
                        httpClient.DefaultRequestHeaders.Add("CorrelationId", Guid.NewGuid().ToString());
                    })
                    .AddTypedClient(client => RestService.For<ICalculatorHttpClient>(client));

                    services.AddTransient<ConsoleHttpClient>();
                    services.AddTransient<ConsoleHttpNamedClient>();
                    services.AddTransient<ConsoleHttpTypedClient>();
                    //services.AddTransient<ConsoleRefitClient>();
                    services.AddTransient<ConsoleAllVerbsHttpCLient>();
                    //services.AddTransient<ICalculatorHttpClient, CorrelationIdCalculatorHttpClient>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var service = services.GetRequiredService<ConsoleAllVerbsHttpCLient>();
                    var result = await service.Run(args);

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
            }

            Console.ReadKey();

            return 0;
        }
    }
}
