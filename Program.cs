


using Microsoft.Extensions.Configuration;
using Serilog.Context;
using Serilog;

namespace cli;

public class Program
{
    const string ENV_PREFIX = "CLI_TOOL_";
    public static void Main(string[] args)
    {

        #region  App Configuration Setup and Test  

        var config = SetConfiguration();

        var envInfo = new EnvInfo();

        config.Bind("EnvInfo", envInfo);

        Console.WriteLine(envInfo.greeting);

        #endregion


        #region  Set Serilog and Test 

        SetSerilog(config);

        Console.WriteLine(envInfo.machine);

        using (LogContext.PushProperty("Method", "Main Method"))
        {

            Log.Information("from logger info");

            Log.Debug("from logger debug");

            Log.Error(new Exception("Test Exception it should be second line due to format exception"), "from logger error");
        }

        
        ILogger log = Log.ForContext<Program>();// Log.ForContext(typeof(Program)); 

        log.Information("Test Information "); 

        #endregion

    }

    #region  Configration Helper Methods
    public static IConfiguration SetConfiguration()
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
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithThreadName()
                    .CreateLogger();
    }

    #endregion
}
