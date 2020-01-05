using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddConfiguration(this IConfigurationBuilder configurationBuilder, HostBuilderContext hostingContext, string[] args)
        {
            string environmentName = hostingContext.HostingEnvironment.EnvironmentName;
            
            string executableLocation = AppContext.BaseDirectory;
            string pathOfCommonSettingsFile = Path.Combine(executableLocation, "Properties");

            IConfigurationBuilder builtConfig = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, $"appsettings.{environmentName}.json"))
                .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, "logSettings.json"), optional: true)
                .AddCommandLine(args);

            configurationBuilder.AddConfiguration(builtConfig.Build());
        }
    }
}
