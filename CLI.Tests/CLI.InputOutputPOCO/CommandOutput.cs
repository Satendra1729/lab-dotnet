
namespace CLI.InputOutputPOCO;

public class CommandOutput : ICommandOutput
{
    public string[] Command {get;init; }
    public string Output { get;init;}

    public CommandOutput(string[] command,string output){
        this.Command = command; 
        this.Output = output; 
    }
}