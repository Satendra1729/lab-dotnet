
namespace CLI.Utils;
public class ErrorMessage {
    public string FileNotFound = "file not found"; 
    public string InvalidBucketName = "invalid bucket name"; 
    public string NoEcho = "there is nothing to get echo"; 
    public string InvalidFileOptionValue(string option) => $"invalid {option} option value"; 

}