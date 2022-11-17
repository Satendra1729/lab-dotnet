


using Microsoft.Extensions.Configuration;

namespace cli; 

public class Program {

    const string ENV_PREFIX = "CLI_TOOL_"; 
    public static void Main(string[] args){
        var builder = new ConfigurationBuilder(); 

        var config = builder.AddJsonFile("appsettings.json",false,true)

                    .AddEnvironmentVariables(prefix: ENV_PREFIX)

                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("CLI_TOOL_ENV")}.json",true,true).Build(); 

        var envInfo = new EnvInfo(); 

        config.Bind("EnvInfo",envInfo); 
        
        Console.WriteLine(envInfo.greeting); 

        // export CLI_TOOL_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  
        Console.WriteLine(envInfo.machine); 
    }
}

public class EnvInfo {
    public string env {get;set;}

    public string greeting {get;set;}

    public string machine {get;set;}
}