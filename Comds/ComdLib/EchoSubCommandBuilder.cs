
using System.CommandLine;

namespace cli.Comds;

public class EchoSubCommandBuilder : ISubCommandBuilder
{
    private Command _cmd { get; set; }
    private Option<string> _echoOption { get; set; }
    public EchoOptionBuilder echoOptionBuilder {get;set;}
    public ISubCommandBuilder CreateCommand()
    {
        var comand = new Command("test", "this is a test sub command");
        _cmd = comand;
        return this;
    }

    public ISubCommandBuilder AddOptions()
    {
        _echoOption = echoOptionBuilder.CreateOption().Build(); 
        _cmd.AddOption(_echoOption); 
        return this;
    }

    public ISubCommandBuilder AttachHandler()
    {
        _cmd.SetHandler((_echoOptionResult) => { 
            Console.WriteLine($"I am your echo : {_echoOptionResult}"); 
            }, this._echoOption);
        return this;
    }

    public Command Build(){
        return _cmd; 
    }
}

