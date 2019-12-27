using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace CoolCopter.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Exception.ToString());
            };

            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }
            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());
            //await builder.Build().RunAsync();
            if (isService)
            {
                builder.Build().RunAsService();
            }
            else
            {
                await builder.Build().RunAsync();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); // optional extra provider
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddDebug();
                logging.AddConsole();
            })
            .UseKestrel((host, options) =>
            {
                options.Limits.MinRequestBodyDataRate = null;
                var coolCopter = host.Configuration.GetSection("CoolCopter").GetChildren();
#if DEBUG
                foreach (var item in coolCopter)
                    Console.WriteLine($"Printing config {item.Key} - {item.Value}");
#endif
                var port = int.Parse(coolCopter.First(x => x.Key == "Port").Value);
                options.ListenAnyIP(port, listenOptions =>
                {
                    X509Certificate2 cert = null;
                    if (string.IsNullOrWhiteSpace(coolCopter.First(x => x.Key == "SSLCertPassword").Value))
                    {
                        cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "Cert",
                            coolCopter.First(x => x.Key == "SSLCertName").Value));
                    }
                    else
                    {
                        cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "Cert",
                            coolCopter.First(x => x.Key == "SSLCertName").Value),
                            coolCopter.First(x => x.Key == "SSLCertPassword").Value);
                    }
                    listenOptions.UseHttps(cert);
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });
            })
            .UseStartup<Startup>();
    }
}
