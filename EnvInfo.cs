using System.Text.Json;
namespace cli;

public class EnvInfo
{
    public string env { get; set; }

    public string greeting { get; set; }

    public string machine { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize<EnvInfo>(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
