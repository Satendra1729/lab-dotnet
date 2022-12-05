using System.CommandLine;

namespace cli.Comds;


public class Root : IRoot
{
    public FileOptionBuilder fileOptionBuilder { get; set; }

    public EchoSubCommandBuilder echoSubCommandBuilder { get; set; }

    public SearchOptionBuilder searchOptionBuilder { get; set; }
    public void AttachRootOptionsAndHandler(RootCommand rootCommand)
    {
        // root comands options
        var fileOption = AddFileOption(rootCommand);

        var searchOption = AddSearchOption(rootCommand);

        rootCommand.SetHandler(RootHandler, fileOption, searchOption);

    }

    public void AttachSubCommands(RootCommand rootCommand)
    {
        var testComand = echoSubCommandBuilder
                                        .CreateCommand()
                                        .AddOptions()
                                        .AttachHandler()
                                        .Build();

        rootCommand.AddCommand(testComand);
    }

    private Option<FileInfo> AddFileOption(RootCommand rootCommand)
    {
        var fileOption = fileOptionBuilder
                             .CreateOption()
                             .AttachValidator()
                             .Build();

        fileOption.IsRequired = true;

        rootCommand.AddOption(fileOption);

        return fileOption;

    }

    private Option<string> AddSearchOption(RootCommand rootCommand)
    {
        var searchOption = searchOptionBuilder
                               .CreateOption()
                               .Build();

        rootCommand.AddOption(searchOption);

        return searchOption;
    }
    private void RootHandler(FileInfo fileInfo, string searchOption)
    {

        var fileLines = File.ReadLines(fileInfo.FullName);

        if (!string.IsNullOrWhiteSpace(searchOption))
            fileLines = fileLines
            .Where(x => x.Contains(searchOption));

        fileLines.ToList().ForEach(x => Console.WriteLine(x));

    }

}