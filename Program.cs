


using Microsoft.Extensions.Configuration;
using Serilog.Context;
using Serilog;
using Autofac;
using AutofacSerilogIntegration;

namespace cli;
public class Program
{
    const string ENV_PREFIX = "CLI_TOOL_";
    public static void Main(string[] args)
    {

        var config = GetConfiguration();

        SetSerilog(config);

        IContainer container = GetIoC(config);

        using (var scope = container.BeginLifetimeScope())
        {

            scope.Resolve<Application>().Run();

        }


    }

    #region  Configration Helper Methods
    public static IConfiguration GetConfiguration()
    {

        var builder = new ConfigurationBuilder();

        // export CLI_TOOL_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  

        var config = builder.AddJsonFile("appsettings.json", false, true)

                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(ENV_PREFIX + "ENV")}.json", true, true)

                    .AddEnvironmentVariables(prefix: ENV_PREFIX)

                    .Build();

        return config;

    }

    public static void SetSerilog(IConfiguration config)
    {
        // Serilog config 
        // to orverride the log level use :: export CLI_TOOL_Serilog__MinimumLevel=Information
        Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithThreadName()
                    .CreateLogger();
    }

    public static IContainer GetIoC(IConfiguration config)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterLogger();

        containerBuilder.RegisterInstance(config.GetSection("envInfo").Get<EnvInfo>()).AsSelf();

        containerBuilder.RegisterType<Application>().AsSelf();

        // register cumtom types as application grows

        var container = containerBuilder.Build();

        return container;
    }
    #endregion

}
