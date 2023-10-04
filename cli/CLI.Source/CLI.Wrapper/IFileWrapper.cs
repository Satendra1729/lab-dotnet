

namespace CLI.Wrapper; 

public interface IFileWrapper{
    IEnumerable<string> ReadLines(string fileName); 
}