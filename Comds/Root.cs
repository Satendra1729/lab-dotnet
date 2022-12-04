using System.CommandLine;

namespace cli.Comds;


public class Root : IRoot
{
    private ErrorMessage _errorMessage {get;init; } 

    public Root(ErrorMessage errorMessage){
        _errorMessage = errorMessage; 
    }
    public void AttachRootOptionsAndHandler(RootCommand rootCommand)
    {
        var fileOption = AddFileOption(rootCommand); 

        var searchOption = AddSearchOption(rootCommand); 

        rootCommand.SetHandler(RootHandler, fileOption,searchOption);
    }

    private Option<FileInfo> AddFileOption(RootCommand rootCommand)
    {
        var fileOption = new FileOptionBuilder(_errorMessage)
                             .CreateOption()
                             .AttachValidator()
                             .Build(); 

        rootCommand.AddOption(fileOption);

        return fileOption; 

    }

    private Option<string> AddSearchOption(RootCommand rootCommand)
    {
        var searchOption = new SearchOptionBuilder()
                               .CreateOption()
                               .Build(); 

        rootCommand.AddOption(searchOption); 

        return searchOption; 
    }
    private void RootHandler(FileInfo fileInfo,string searchOption)
    {
        File.ReadLines(fileInfo.FullName)
            .Where(x => x.Contains(searchOption))
            .ToList()
            .ForEach(line => Console.WriteLine(line));
    }
    
}