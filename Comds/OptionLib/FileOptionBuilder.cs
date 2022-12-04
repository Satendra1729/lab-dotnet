

using System.CommandLine;
using System.CommandLine.Parsing;

namespace cli.Comds; 

public class FileOptionBuilder : IOptionBuilder<FileInfo>
{

    private Option<FileInfo> _fileOption;
    public IOptionBuilder<FileInfo> CreateOption()
    {
        var fileOption = new Option<FileInfo>(name: "--file", description: "file to read");

        fileOption.AddAlias("-f");

        _fileOption = fileOption; 

        return this; 
    }
    public IOptionBuilder<FileInfo> AttachValidator()
    {
        this._fileOption.AddValidator(Validate); 
        
        return this; 
    }

    public Option<FileInfo> Build() {
        return _fileOption; 
    }

    
    private void  Validate(OptionResult optionResult)
    {
        if (!File.Exists(optionResult.GetValueForOption(this._fileOption).FullName))
        {
            optionResult.ErrorMessage = "File Does not exits";
        }
        
    }
}