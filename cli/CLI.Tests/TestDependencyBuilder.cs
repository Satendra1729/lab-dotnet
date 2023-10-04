
using CLI.Tests.Mocks;
using CLI.Wrapper;


namespace CLI.Tests; 
public class TestDependencyBuilder : DependencyBuilder
{
    public  override IDependencyBuilder AddConfiguration()
    {
         var builder = new ConfigurationBuilder();

        // export CLI_EnvInfo__machine="machine name"   ::  prefix plus key value sperated by double undersore  

        base._config = builder.AddJsonFile("CLI.Configs/appsettings.json", false, true)
                    .Build();

        return this;
    }

    public override IDependencyBuilder AddExternalDependencies()
    {
        base.AddExternalDependencies();

        base._containerBuilder.RegisterType<FileWrapperMock>().As<IFileWrapper>();

         _containerBuilder.RegisterType<Amazons3Client>().As<IAmazonS3>().SingleInstance();
        return this;  
    }
}