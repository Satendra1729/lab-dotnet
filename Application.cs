
using Serilog;

namespace cli;
public class Application
{


    private ILogger _logger { get; init; }

    private EnvInfo _envInfo { get; init; }
    public Application(ILogger logger, EnvInfo envInfo)
    {
        _logger = logger;
        _envInfo = envInfo;
    }
    public void Run()
    {
        _logger.Information("Application started with env Configuration " + Environment.NewLine + _envInfo);

        _logger.Information("Application doing something");

        Thread.Sleep(5000);

        _logger.Information("Application stopping....");
    }
}