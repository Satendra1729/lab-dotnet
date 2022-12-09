
using System.CommandLine; 

namespace CLI.Cmd;

public interface ISubCommandBuilder {

    ISubCommandBuilder CreateCommand();

    ISubCommandBuilder AddOptions(); 

    ISubCommandBuilder AttachHandlerWithExceptionHandler();  

    Command Build(); 
}