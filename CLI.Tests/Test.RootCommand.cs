namespace CLI.Tests;

public class RootCommandTest
{
    StringWriter sw = new StringWriter();
    IContainer _container { get; set; }

    [SetUp]
    public void Setup()
    {
        Console.SetOut(sw);
        _container = new TestDependencyBuilder()
                            .AddConfiguration()
                            .AddLogger()
                            .AddInternalDependencies()
                            .AddExternalDependencies()
                            .Build();
    }

    [Test]
    public void RootCommandWithFile()
    {

        var commandOutput = CommandOutputMap.GetCommandOutput(CommandName.RootWithValidFile);

        using (var scope = _container.BeginLifetimeScope())
        {
            var app = scope.Resolve<Application>().Run(commandOutput.Command).Result;
        }

        var outputString = sw.ToString();

        sw.GetStringBuilder().Remove(0, outputString.Length);

        Assert.That(outputString, Is.EqualTo(commandOutput.Output));

    }

    [Test]
    public void RootWithValidFileAndSearchFilter()
    {
        var commandOutput = CommandOutputMap.GetCommandOutput(CommandName.RootWithValidFileAndSearchFilter);

        using (var scope = _container.BeginLifetimeScope())
        {
            var app = scope.Resolve<Application>().Run(commandOutput.Command).Result;
        }

        var outputString = sw.ToString();

        sw.GetStringBuilder().Remove(0, outputString.Length);

        Assert.That(outputString, Is.EqualTo(commandOutput.Output));
    }
}
