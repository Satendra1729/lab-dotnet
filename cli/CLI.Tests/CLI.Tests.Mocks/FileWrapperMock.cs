

using CLI.Wrapper; 

namespace CLI.Tests.Mocks; 
public class FileWrapperMock : IFileWrapper
{
    public IEnumerable<string> ReadLines(string fileName)
    {
       return new string[]{"test line1","test line2"}; 
    }
}