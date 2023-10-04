


namespace CLI.Wrapper; 
public class FileWrapper : IFileWrapper {
    public IEnumerable<string> ReadLines(string fileName){
        return File.ReadLines(fileName); 
    }
}