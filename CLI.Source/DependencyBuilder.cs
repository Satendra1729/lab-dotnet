
using Autofac;
using Serilog; 
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Amazon.S3;

using CLI.Cmd;
using CLI.Utils; 
using CLI.Option; 
using CLI.POCO; 
using CLI.Wrapper; 

namespace CLI;

public class DependencyBuilder : IDependencyBuilder
{
    const string ENV_PREFIX = "CLI_";
    protected ContainerBuilder _containerBuilder {get;init;}
    protected IConfigurationRoot _config {get;set;}
    public DependencyBuilder()
    {
        _containerBuilder = new ContainerBuilder(); 
    }
    public virtual IDependencyBuilder AddConfiguration()
    {
        var builder = new ConfigurationBuilder();

        // export CLI_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  

        _config = builder.AddJsonFile("CLI.Configs/appsettings.json", false, true)

                    .AddJsonFile($"CLI.Configs/appsettings.{Environment.GetEnvironmentVariable(ENV_PREFIX + "ENV")}.json", true, true)

                    .AddEnvironmentVariables(prefix: ENV_PREFIX)

                    .Build();

        return this;
    }
    public virtual IDependencyBuilder AddLogger()
    {
        // to orverride the log level use :: export CLI_Serilog__MinimumLevel=Information
        Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(_config)
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithThreadName()
                    .CreateLogger();
        _containerBuilder.RegisterLogger();
        return this;

    }  
    public virtual IDependencyBuilder AddInternalDependencies()
    {
        _containerBuilder.RegisterType<ErrorMessage>().SingleInstance();

        // command option injection 
        _containerBuilder.RegisterType<FileOptionBuilder>().As<IOptionBuilder<FileInfo>>().AsSelf();

        _containerBuilder.RegisterType<EchoOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();

        _containerBuilder.RegisterType<SearchOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();

        _containerBuilder.RegisterType<S3BucketNameOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();
        // subcommands 
        _containerBuilder.RegisterType<EchoSubCommandBuilder>().As<EchoSubCommandBuilder>().OnActivated((e) =>
        {
            e.Instance.echoOptionBuilder = e.Context.Resolve<EchoOptionBuilder>();
        });

        _containerBuilder.RegisterType<AWSS3SubCommandBuilder>().As<AWSS3SubCommandBuilder>().OnActivated(e =>
        {
            e.Instance._s3BucketNameOptionBuilder = e.Context.Resolve<S3BucketNameOptionBuilder>();
        });
        // root command
        _containerBuilder.RegisterType<Root>().As<IRoot>().OnActivated(e =>
        {
            e.Instance.fileOptionBuilder = e.Context.Resolve<FileOptionBuilder>();
            e.Instance.echoSubCommandBuilder = e.Context.Resolve<EchoSubCommandBuilder>();
            e.Instance.searchOptionBuilder = e.Context.Resolve<SearchOptionBuilder>();
            e.Instance.aWSS3CommandBuilder = e.Context.Resolve<AWSS3SubCommandBuilder>();
        });
        // application entry Point  
        _containerBuilder.Register((ctx) =>
                        new Application(ctx.Resolve<ILogger>(),
                                        ctx.Resolve<Settings>(),
                                        ctx.Resolve<IRoot>())).SingleInstance();

        return this; 
    }
    public virtual IDependencyBuilder AddExternalDependencies()
    {
            // Envinfo settings 
            _containerBuilder.RegisterInstance(_config.GetSection("envInfo").Get<Settings>()).AsSelf();

            // AWS S3 Client 
            var options = _config.GetAWSOptions();

            IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();

            _containerBuilder.Register(ctx => client).As<IAmazonS3>().SingleInstance();

            // File 
            _containerBuilder.RegisterType<FileWrapper>().As<IFileWrapper>(); 

            return this;
    }

 

    public  virtual IContainer Build()
    {
       return _containerBuilder.Build(); 
    }
}