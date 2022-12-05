

using System.CommandLine;
using Amazon.S3;
using Amazon.S3.Model;

namespace cli.Comds;


public class AWSS3CommandBuilder : ISubCommandBuilder
{

    private Command _cmd { get; set; }

    private Option<string> _bucketName { get; set; }

    private IAmazonS3 _s3Client { get; set; }

    public AWSS3CommandBuilder(IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
    }
    public ISubCommandBuilder CreateCommand()
    {
        _cmd = new Command("awss3", "work with aws");
        return this;
    }
    public ISubCommandBuilder AddOptions()
    {
        _bucketName = new Option<string>(new string[] { "--bucket", "-b" }, "s3 bucket name");
        _cmd.AddOption(_bucketName);
        return this;
    }

    public ISubCommandBuilder AttachHandler()
    {
        _cmd.SetHandler((bucketNameResult) =>
        {
            Console.WriteLine("bucketNameResult => ",bucketNameResult); 
            _s3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = bucketNameResult,
                MaxKeys = 5,
            }).Result.S3Objects.ForEach(x => Console.WriteLine(x.Key));

        }, _bucketName);
        return this;
        //_s3Client.ListBucketsAsync().Result.Buckets.ForEach(x => Console.WriteLine(x.BucketName));
    }

    public Command Build()
    {
        return this._cmd;
    }


}