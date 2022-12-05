

using System.CommandLine;
using Amazon.S3;
using Amazon.S3.Model;

namespace cli.Comds;


public class AWSS3SubCommandBuilder : ISubCommandBuilder
{

    private Command _cmd { get; set; }

    private Option<string> _bucketName { get; set; }

    private IAmazonS3 _s3Client { get; set; }

    public S3BucketNameOptionBuilder _s3BucketNameOptionBuilder { get; set; }

    public AWSS3SubCommandBuilder(IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
    }
    public ISubCommandBuilder CreateCommand()
    {
        _cmd = new Command("awss3", "aws s3");
        return this;
    }
    public ISubCommandBuilder AddOptions()
    {
        _bucketName = _s3BucketNameOptionBuilder
                            .CreateOption()
                            .AttachValidator()
                            .Build();

        _cmd.AddOption(_bucketName);
        return this;
    }

    public ISubCommandBuilder AttachHandlerWithExceptionHandler()
    {
        _cmd.SetHandler((bucketNameResult) =>
        {
           try {
            _s3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = bucketNameResult,
                MaxKeys = 5,
            }).Result.S3Objects.ForEach(x => Console.WriteLine(x.Key));
           }
           catch(Exception ex){
            Console.WriteLine(ex.Message); 
           }

        }, _bucketName);
        return this;
        //_s3Client.ListBucketsAsync().Result.Buckets.ForEach(x => Console.WriteLine(x.BucketName));
    }

    public Command Build()
    {
        return this._cmd;
    }


}