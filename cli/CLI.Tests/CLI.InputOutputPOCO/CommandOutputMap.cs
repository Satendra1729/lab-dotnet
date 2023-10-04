namespace CLI.InputOutputPOCO;


public enum CommandName{
    RootWithValidFile,RootWithValidFileAndSearchFilter,awss3BucketObjectList
}
public class CommandOutputMap
{
    private static Dictionary<CommandName,ICommandOutput> commandOutputsMap = new Dictionary<CommandName, ICommandOutput>();

    static CommandOutputMap(){

        commandOutputsMap[CommandName.RootWithValidFile] = new CommandOutput(new string[] { "--file", "CLI.Configs/appsettings.json" }, $"test line1{Environment.NewLine}test line2{Environment.NewLine}");

        commandOutputsMap[CommandName.RootWithValidFileAndSearchFilter]= new CommandOutput(new string[] { "--file", "CLI.Configs/appsettings.json","-s","line1" }, $"test line1{Environment.NewLine}");

        commandOutputsMap[CommandName.awss3BucketObjectList] = new CommandOutput(new string[] { "awss3", "-b", "satendrakk.com" },$"key1{Environment.NewLine}key2{Environment.NewLine}key3{Environment.NewLine}"); 

    }
    public static ICommandOutput GetCommandOutput(CommandName key)
    {
        return commandOutputsMap[key];      
    }

}
