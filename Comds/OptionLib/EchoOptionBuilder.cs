
using System.CommandLine;

namespace cli.Comds;


public class EchoOptionBuilder : IOptionBuilder<string>
{

    public Option<string> _echoOption {get;set;}

      public IOptionBuilder<string> CreateOption()
    {
         _echoOption = new Option<string>(new string[]{"--echo","-e"},"echo option"); 
         return this; 
    }
    public IOptionBuilder<string> AttachValidator()
    {
       return this; 
    }

    public Option<string> Build()
    {
        return _echoOption; 
    }

  
}