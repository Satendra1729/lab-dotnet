using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Binding;

namespace cli.Comds;


public class Root : IRoot
{
    public void AttachRootOptionsAndHandler(RootCommand rootCommand)
    {
        AddFileOption(rootCommand); 

    }

    private void AddFileOption(RootCommand rootCommand)
    {
        var fileOption = new FileOptionBuilder()
                             .CreateOption()
                             .AttachValidator()
                             .Build(); 

        rootCommand.AddOption(fileOption);

        rootCommand.SetHandler(RootHandler, fileOption);
    }
    private void RootHandler(FileInfo fileInfo)
    {
        File.ReadLines(fileInfo.FullName).ToList()
            .ForEach(line => Console.WriteLine(line));
    }
    
}