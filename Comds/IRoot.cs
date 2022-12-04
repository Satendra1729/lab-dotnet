using System.CommandLine; 

namespace cli.Comds; 

public interface IRoot {
    public void AttachRootOptionsAndHandler(RootCommand rootCommand); 
}