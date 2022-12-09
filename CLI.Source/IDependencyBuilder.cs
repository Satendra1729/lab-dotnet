using Autofac; 

namespace CLI;
public interface IDependencyBuilder {

    IDependencyBuilder AddConfiguration(); 
    IDependencyBuilder AddLogger(); 
    IDependencyBuilder AddInternalDependencies(); 
    IDependencyBuilder AddExternalDependencies(); 
    IContainer Build();
}