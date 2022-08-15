using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FunctionApps.Schedule;

[assembly: FunctionsStartup(typeof(FunctionApps.SubscriptionRenewScheduler.Startup))]

namespace FunctionApps.SubscriptionRenewScheduler
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            // Note that these files are not automatically copied on build or publish.
            // See the csproj file to for the correct setup.
            builder.ConfigurationBuilder
                .AddJsonFile(
                    Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                    optional: true,
                    reloadOnChange: false
                )
                .AddJsonFile(
                    Path.Combine(
                        context.ApplicationRootPath,
                        $"appsettings.{context.EnvironmentName}.json"
                    ),
                    optional: true,
                    reloadOnChange: false
                )
                .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            // Register IOptions using the custom SchedulerOptions.
            FunctionsHostBuilderContext context = builder.GetContext();
            builder.Services.Configure<SchedulerOptions>(context.Configuration);
        }
    }
}
