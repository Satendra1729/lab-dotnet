
using Serilog;

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

        _logger.Information("Application doing something");

        _logger.Information(String.Join(",",_args)); 

        Thread.Sleep(5000);

        _logger.Information("Application stopping....");
    }
}