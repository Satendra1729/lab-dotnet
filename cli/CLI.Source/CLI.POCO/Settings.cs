using System.Text.Json;
namespace CLI.POCO;

public class Settings
{
    public string env { get; set; }

    public string greeting { get; set; }

    public string machine { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize<Settings>(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
