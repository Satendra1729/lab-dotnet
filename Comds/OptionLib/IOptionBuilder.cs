

using System.CommandLine; 
using System.CommandLine.Parsing; 

namespace cli.Comds; 
public interface IOptionBuilder<T> {


    IOptionBuilder<T> CreateOption(); 

    IOptionBuilder<T> AttachValidator(); 

    Option<T> Build(); 

}
