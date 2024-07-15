using DataHubService.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Wfs.Model.Helper;

namespace DataHubService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var enviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{enviromentName}.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            var startup = new Startup(configuration);
            startup.ConfigurationServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var applicationConfiguration = serviceProvider.GetService<IApplicationConfiguration>();


            var commnad = args[0].Trim().ToUpperInvariant();

            if (string.Compare(commnad, applicationConfiguration.ProgramJobName.DataHubUpStream, StringComparison.OrdinalIgnoreCase) == 0)
            {
                var dataHubUpStreamWorkerProgram = serviceProvider.GetService<IDataHubUpStreamImportWorkerProgram>();
                dataHubUpStreamWorkerProgram.Run();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
