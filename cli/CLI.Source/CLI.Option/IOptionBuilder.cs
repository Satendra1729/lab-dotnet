

using System.CommandLine; 

namespace CLI.Option;
public interface IOptionBuilder<T> {

    IOptionBuilder<T> CreateOption(); 

    IOptionBuilder<T> AttachValidator(); 

    Option<T> Build(); 

}
