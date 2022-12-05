
using Microsoft.Extensions.Configuration;
using Serilog;
using Autofac;
using AutofacSerilogIntegration;
using cli.Comds;
using Amazon;
using Amazon.S3;

namespace cli;
public class Program
{
    const string ENV_PREFIX = "CLI_TOOL_";
    public static void Main(string[] args)
    {

        var config = GetConfiguration();

        SetSerilog(config);

        IContainer container = GetIoC(config, args);

        using (var scope = container.BeginLifetimeScope())
        {
            scope.Resolve<Application>().Run();
        }


    }

    #region  Configration Helper Methods
    public static IConfiguration GetConfiguration()
    {

        var builder = new ConfigurationBuilder();

        // export CLI_TOOL_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  

        var config = builder.AddJsonFile("configs/appsettings.json", false, true)

                    .AddJsonFile($"configs/appsettings.{Environment.GetEnvironmentVariable(ENV_PREFIX + "ENV")}.json", true, true)

                    .AddEnvironmentVariables(prefix: ENV_PREFIX)

                    .Build();

        return config;

    }

    public static void SetSerilog(IConfiguration config)
    {
        // Serilog config 
        // to orverride the log level use :: export CLI_TOOL_Serilog__MinimumLevel=Information
        Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithThreadName()
                    .CreateLogger();
    }

    public static IContainer GetIoC(IConfiguration config, string[] args)
    {
        var containerBuilder = new ContainerBuilder();

        // logging 
        containerBuilder.RegisterLogger();
        // env info settings 
        containerBuilder.RegisterInstance(config.GetSection("envInfo").Get<EnvInfo>()).AsSelf();
        // utillity 
        containerBuilder.RegisterType<ErrorMessage>().SingleInstance();

        var options = config.GetAWSOptions();

        IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();

        containerBuilder.Register(ctx => client).As<IAmazonS3>().SingleInstance();

        // command option injection 
        containerBuilder.RegisterType<FileOptionBuilder>().As<IOptionBuilder<FileInfo>>().AsSelf();

        containerBuilder.RegisterType<EchoOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();

        containerBuilder.RegisterType<SearchOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();

        containerBuilder.RegisterType<S3BucketNameOptionBuilder>().As<IOptionBuilder<string>>().AsSelf();
        // subcommands 
        containerBuilder.RegisterType<EchoSubCommandBuilder>().As<EchoSubCommandBuilder>().OnActivated((e) =>
        {
            e.Instance.echoOptionBuilder = e.Context.Resolve<EchoOptionBuilder>();
        });

        containerBuilder.RegisterType<AWSS3SubCommandBuilder>().As<AWSS3SubCommandBuilder>().OnActivated(e =>
        {
            e.Instance._s3BucketNameOptionBuilder = e.Context.Resolve<S3BucketNameOptionBuilder>();
        });
        // root command
        containerBuilder.RegisterType<Root>().As<IRoot>().OnActivated(e =>
        {
            e.Instance.fileOptionBuilder = e.Context.Resolve<FileOptionBuilder>();
            e.Instance.echoSubCommandBuilder = e.Context.Resolve<EchoSubCommandBuilder>();
            e.Instance.searchOptionBuilder = e.Context.Resolve<SearchOptionBuilder>();
            e.Instance.aWSS3CommandBuilder = e.Context.Resolve<AWSS3SubCommandBuilder>();
        });
        // application entry Point  
        containerBuilder.Register((ctx) =>
                        new Application(ctx.Resolve<ILogger>(),
                                        ctx.Resolve<EnvInfo>(),
                                        args,
                                        ctx.Resolve<IRoot>())).SingleInstance();


        // build container 
        var container = containerBuilder.Build();

        return container;
    }
    #endregion

}
