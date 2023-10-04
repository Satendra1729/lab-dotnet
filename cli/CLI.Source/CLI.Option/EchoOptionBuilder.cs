
using System.CommandLine;
using System.CommandLine.Parsing;
using CLI.Utils; 

namespace CLI.Option;


public class EchoOptionBuilder : IOptionBuilder<string>
{
    public Option<string> _echoOption {get;set;}

    public ErrorMessage _errorMessage {get;init;}
    public EchoOptionBuilder(ErrorMessage errorMessage){
      _errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage)); 
    }

      public IOptionBuilder<string> CreateOption()
    {
         _echoOption = new Option<string>(new string[]{"--echo","-e"},"echo option"); 
         _echoOption.SetDefaultValue(""); 
         return this; 
    }
    public IOptionBuilder<string> AttachValidator()
    {
      this._echoOption.AddValidator((OptionResult optionResult) => {
        if(string.IsNullOrWhiteSpace(optionResult.GetValueForOption(this._echoOption)))
            optionResult.ErrorMessage = _errorMessage.NoEcho; 
      });  
       return this; 
    }

    public Option<string> Build()
    {
        return _echoOption; 
    }

  
}