


using Microsoft.Extensions.Configuration;
using Serilog; 

namespace cli; 

public class Program {

     
    public class EnvInfo {
        public string env {get;set;}

        public string greeting {get;set;}

        public string machine {get;set;}
    }

    const string ENV_PREFIX = "CLI_TOOL_"; 
    public static void Main(string[] args){
        var builder = new ConfigurationBuilder(); 

        var config = builder.AddJsonFile("appsettings.json",false,true)

                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(ENV_PREFIX+"ENV")}.json",true,true)

                    .AddEnvironmentVariables(prefix: ENV_PREFIX)
                    
                    .Build(); 

        var envInfo = new EnvInfo(); 

        config.Bind("EnvInfo",envInfo); 
        
        Console.WriteLine(envInfo.greeting); 

        // export CLI_TOOL_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  
        Console.WriteLine(envInfo.machine); 



        // Serilog config 
        // to orverride the log level use :: xport CLI_TOOL_Serilog__MinimumLevel=Information
        Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();
        
        Log.Information("from logger info");
        Log.Debug("from logger info");
        Log.Error("from logger info");

        
    }
}
