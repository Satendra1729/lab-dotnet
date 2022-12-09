using System.CommandLine; 

namespace CLI.Cmd;

public interface IRoot {
    void AttachRootOptionsAndHandler(RootCommand rootCommand); 

    void AttachSubCommands(RootCommand rootCommand); 
}