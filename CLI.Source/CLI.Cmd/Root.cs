using System.CommandLine;
using CLI.Option; 
using CLI.Wrapper; 

namespace CLI.Cmd;


public class Root : IRoot
{
    private IFileWrapper _fileWrapper {get;set;}
    public FileOptionBuilder fileOptionBuilder { get; set; }

    public SearchOptionBuilder searchOptionBuilder { get; set; }

    public EchoSubCommandBuilder echoSubCommandBuilder { get; set; }

    public AWSS3SubCommandBuilder aWSS3CommandBuilder { get; set; }


    public Root(IFileWrapper fileWrapper)
    {
        _fileWrapper = fileWrapper ?? throw new ArgumentNullException(nameof(fileWrapper));  
    }
    public void AttachRootOptionsAndHandler(RootCommand rootCommand)
    {
        // root comands options
        var fileOption = AddFileOption(rootCommand);

        var searchOption = AddSearchOption(rootCommand);

        rootCommand.SetHandler(RootHandler, fileOption, searchOption);

    }

    public void AttachSubCommands(RootCommand rootCommand)
    {
        var testCommand = echoSubCommandBuilder
                                        .CreateCommand()
                                        .AddOptions()
                                        .AttachHandlerWithExceptionHandler()
                                        .Build();

        var awss3Command = aWSS3CommandBuilder
                            .CreateCommand()
                            .AddOptions()
                            .AttachHandlerWithExceptionHandler()
                            .Build();

        rootCommand.AddCommand(testCommand);

        rootCommand.AddCommand(awss3Command);
    }

    private Option<FileInfo> AddFileOption(RootCommand rootCommand)
    {
        var fileOption = fileOptionBuilder
                             .CreateOption()
                             .AttachValidator()
                             .Build();

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
    private async Task<int> RootHandler(FileInfo fileInfo, string searchOption)
    {
        try
        {
            var fileLines = _fileWrapper.ReadLines(fileInfo.FullName);

            if (!string.IsNullOrWhiteSpace(searchOption))
                fileLines = fileLines
                            .Where(x => x.Contains(searchOption));

            fileLines
               .ToList()
               .ForEach(x => Console.WriteLine(x));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return await Task.FromResult<int>(1);
        }
        return await Task.FromResult<int>(0);

    }

}