using System.Text.Json.Serialization;

namespace BodylogExtender.presets;

public class PresetState
{
    [JsonInclude]
    public int Index;
    [JsonInclude] 
    public Dictionary<int, AvatarPreset> Presets = new();
}