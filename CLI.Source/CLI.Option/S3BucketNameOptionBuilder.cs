

using System.CommandLine;
using System.CommandLine.Parsing; 
using CLI.Utils; 

namespace CLI.Option;

public class S3BucketNameOptionBuilder : IOptionBuilder<string>
{
    private Option<string> _bucketNameOption {get;set;}

    private ErrorMessage _errorMessage {get;set;}

    public S3BucketNameOptionBuilder(ErrorMessage errorMessage)
    {
        _errorMessage = errorMessage; 
    }

    public IOptionBuilder<string> CreateOption()
    {
        _bucketNameOption = new Option<string>(new string[]{"--bucket","-b"},"s3 bucket name");
        return this;
    }
    public IOptionBuilder<string> AttachValidator()
    {
       _bucketNameOption.AddValidator(Validate); 
       return this;
    }

    public Option<string> Build()
    {
        return this._bucketNameOption;
    }

    private void  Validate(OptionResult optionResult)
    {
        var  bucketName = optionResult.GetValueForOption(this._bucketNameOption); 

        if (string.IsNullOrEmpty(bucketName))
        {
            optionResult.ErrorMessage = _errorMessage.InvalidBucketName; 
        }
        
    }
   
}