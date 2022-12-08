

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
        _fileOption = new Option<FileInfo>(new string[]{"--file","-f"}, description: "file to read");

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
        var optionValue = optionResult.GetValueForOption(this._fileOption); 
        if (optionValue is null)
        {
           optionResult.ErrorMessage = _errorMessage.InvalidFileOptionValue(this._fileOption.Name); 
        }
        else if(!optionValue.Exists)
        {
             optionResult.ErrorMessage = _errorMessage.FileNotFound; 
        }
        
    }
}