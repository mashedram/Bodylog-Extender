using System.Text.Json.Serialization;

namespace BodylogExtender.AvatarData;

public class PresetState
{
    [JsonInclude]
    public int Index;
    [JsonInclude] 
    public Dictionary<int, AvatarPreset> Presets = new();
}