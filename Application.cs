
using Serilog;
using System.CommandLine;

namespace cli;
public class Application
{

    private ILogger _logger { get; init; }
    private EnvInfo _envInfo { get; init; }
    private string[] _args { get; set; }
    public Application(ILogger logger, EnvInfo envInfo, string[] args)
    {
        _logger = logger;
        _envInfo = envInfo;
        _args = args;
    }
    public void Run()
    {
        _logger.Information("Application started with env Configuration " + Environment.NewLine + _envInfo);

        _logger.Information("Application started with env Configuration " + Environment.NewLine + String.Join(",", _args));

        root(); 

        _logger.Information("Application stopping....");
    }

    private void root()
    {
        var rootCommand = new RootCommand("dotnet cli app");

        var fileOption = new Option<FileInfo>(name: "--file", description: "file to read");

        fileOption.AddAlias("-f"); 

        rootCommand.AddOption(fileOption);

        rootCommand.SetHandler((file) =>
        {
            File.ReadLines(file.FullName).ToList()
            .ForEach(line => Console.WriteLine(line));

        }, fileOption);

        rootCommand.Invoke(_args); 

    }
}