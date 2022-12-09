
using Microsoft.Extensions.Configuration;
using Serilog;
using Autofac;
using AutofacSerilogIntegration;
using cli.Comds;
using Amazon.S3;

namespace cli;
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
