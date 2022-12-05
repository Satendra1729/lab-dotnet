using System.CommandLine; 

namespace cli.Comds; 

public interface IRoot {
    void AttachRootOptionsAndHandler(RootCommand rootCommand); 

    void AttachSubCommands(RootCommand rootCommand); 
}