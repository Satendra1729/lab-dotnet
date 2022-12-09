

using Serilog;
using System.CommandLine;
using CLI.Cmd;
using CLI.POCO;

namespace CLI;
public class Application
{

    private ILogger _logger { get; init; }
    private Settings _envInfo { get; init; }
    private IRoot _root {get;init;}
    public Application(ILogger logger, Settings envInfo, IRoot root)
    {
        _logger = logger;
        _envInfo = envInfo;
        _root = root;
    }
    public async Task<int> Run(string[] args)
    {
        _logger.Information("Application started with env Configuration " + Environment.NewLine + _envInfo);

        _logger.Information("Application started with env Configuration " + Environment.NewLine + String.Join(",", args));

        var rootCommand = new RootCommand("dotnet cli app");

        _root.AttachRootOptionsAndHandler(rootCommand);

        _root.AttachSubCommands(rootCommand);

        return await rootCommand.InvokeAsync(args);

    }
}