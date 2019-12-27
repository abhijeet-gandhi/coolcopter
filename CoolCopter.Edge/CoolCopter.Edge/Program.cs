using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoolCopter.Edge.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace CoolCopter.Edge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables();
                    var env = context.HostingEnvironment;

                    Console.WriteLine($"Environment is {env.EnvironmentName}");

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration.GetSection("Edge").Get<EdgeConfig>();
                    services.AddSingleton(config);
                    var serverAddress = config.ServerAddress;
                    var httpClientHandler = new HttpClientHandler();
                    // Return `true` to allow certificates that are untrusted/invalid
#if DEBUG
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
#endif
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.BaseAddress = new Uri(serverAddress);

                    var channel = GrpcChannel.ForAddress(serverAddress,
                        new GrpcChannelOptions { HttpClient = httpClient });
                    services.AddSingleton(channel);

                    services.AddTransient<IPingService, PingService>();
                    services.AddTransient<IRegistrationService, RegistrationService>();

                    services.AddHostedService<Worker>();
                });
    }
}
