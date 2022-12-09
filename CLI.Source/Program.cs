using System.Threading.Tasks; 
using Autofac;
namespace CLI;
public class Program
{
    public static async Task<int> Main(string[] args)
    {
        
        var container = new DependencyBuilder()
                            .AddConfiguration()
                            .AddLogger()
                            .AddInternalDependencies()
                            .AddExternalDependencies()
                            .Build();

        using (var scope = container.BeginLifetimeScope())
        {
            return await scope.Resolve<Application>().Run(args);
        }
    }

}
