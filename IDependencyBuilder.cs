using Autofac; 

namespace cli;

public interface IDependencyBuilder {

    IDependencyBuilder AddConfiguration(); 
    IDependencyBuilder AddLogger(); 
    IDependencyBuilder AddInternalDependencies(); 
    IDependencyBuilder AddExternalDependencies(); 
    IContainer Build();
}