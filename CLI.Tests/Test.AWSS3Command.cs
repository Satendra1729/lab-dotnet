

namespace CLI.Tests;

public class AwsS3CommandTest
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
    public void AwsS3Command()
    {

       var commandOutput = CommandOutputMap.GetCommandOutput(CommandName.awss3BucketObjectList);

        using (var scope = _container.BeginLifetimeScope())
        {
            var app = scope.Resolve<Application>().Run(commandOutput.Command).Result;
        }

        var outputString = sw.ToString();

        sw.GetStringBuilder().Remove(0, outputString.Length);

        Assert.That(outputString, Is.EqualTo(commandOutput.Output));
    }
}
