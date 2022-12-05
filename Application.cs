
using Serilog;
using System.CommandLine;
using cli.Comds; 

namespace cli;
public class Application
{

    private ILogger _logger { get; init; }
    private EnvInfo _envInfo { get; init; }
    private string[] _args { get; init; }

    private IRoot _root {get;init;}
    public Application(ILogger logger, EnvInfo envInfo, string[] args,IRoot root)
    {
        _logger = logger;
        _envInfo = envInfo;
        _args = args;
        _root = root; 
    }
    public void Run()
    {
        _logger.Information("Application started with env Configuration " + Environment.NewLine + _envInfo);

        _logger.Information("Application started with env Configuration " + Environment.NewLine + String.Join(",", _args));

        var rootCommand = new RootCommand("dotnet cli app");

        _root.AttachRootOptionsAndHandler(rootCommand); 

        _root.AttachSubCommands(rootCommand); 

        rootCommand.Invoke(_args);         

        _logger.Information("Application stopping....");
    }
}