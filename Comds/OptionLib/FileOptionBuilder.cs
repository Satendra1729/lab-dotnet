

using System.CommandLine;
using System.CommandLine.Parsing;

namespace cli.Comds; 

public class FileOptionBuilder : IOptionBuilder<FileInfo>
{

    private Option<FileInfo> _fileOption;

    private ErrorMessage _errorMessage;
    public FileOptionBuilder(ErrorMessage errorMessage)
    {
        _errorMessage = errorMessage;  
    }
    public IOptionBuilder<FileInfo> CreateOption()
    {
        var fileOption = new Option<FileInfo>(new string[]{"--file","-f"}, description: "file to read");

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
        if (!optionResult.GetValueForOption(this._fileOption).Exists)
        {
            optionResult.ErrorMessage = _errorMessage.FileNotFound; 
        }
        
    }
}