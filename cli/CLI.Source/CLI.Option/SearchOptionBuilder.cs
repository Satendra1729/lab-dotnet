

using System.CommandLine;

namespace CLI.Option;


public class SearchOptionBuilder : IOptionBuilder<string>
{
    private Option<string> _searchOption {get;set; } 
    public IOptionBuilder<string> CreateOption()
    {
       var seachString = new Option<string>(new string[]{"--search","-s"},"search text");

       seachString.SetDefaultValue(""); 

       this._searchOption = seachString; 

       return this;  

    }
    public IOptionBuilder<string> AttachValidator()
    {
       return this; 
    }

    public Option<string> Build()
    {
        return this._searchOption; 
    }

    
}