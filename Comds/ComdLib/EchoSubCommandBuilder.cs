
using System.CommandLine;

namespace cli.Comds;

public class EchoSubCommandBuilder : ISubCommandBuilder
{
    private Command _cmd { get; set; }
    private Option<string> _echoOption { get; set; }
    public EchoOptionBuilder echoOptionBuilder { get; set; }
    public ISubCommandBuilder CreateCommand()
    {
        var comand = new Command("test", "this is a test sub command");
        _cmd = comand;
        return this;
    }

    public ISubCommandBuilder AddOptions()
    {
        _echoOption = echoOptionBuilder.CreateOption().AttachValidator().Build();
        _cmd.AddOption(_echoOption);
        return this;
    }

    public ISubCommandBuilder AttachHandlerWithExceptionHandler()
    {
        _cmd.SetHandler((_echoOptionResult) =>
        {
            try
            {
                Console.WriteLine($"I am your echo : {_echoOptionResult}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Task.FromResult(1);
            }
            return Task.FromResult(0);
        }, this._echoOption);
        return this;
    }

    public Command Build()
    {
        return _cmd;
    }
}

