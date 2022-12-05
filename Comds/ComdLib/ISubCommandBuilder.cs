
using System.CommandLine; 

namespace cli.Comds; 

public interface ISubCommandBuilder {

    ISubCommandBuilder CreateCommand();

    ISubCommandBuilder AddOptions(); 

    ISubCommandBuilder AttachHandlerWithExceptionHandler();  

    Command Build(); 
}