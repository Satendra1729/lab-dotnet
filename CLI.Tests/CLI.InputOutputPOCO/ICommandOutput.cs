
namespace CLI.InputOutputPOCO; 
public interface ICommandOutput
{
    public string[] Command { get; init; }
    public string Output { get; init; }
}