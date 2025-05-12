using System.Text.Json.Serialization;
using BodylogExtender.globals;

namespace BodylogExtender.presets;

public class PresetState
{
    [JsonInclude]
    public int Index;
    [JsonInclude]
    public int Size;
    [JsonInclude] 
    public List<AvatarPreset> Presets;

    public PresetState(int index, int size, List<AvatarPreset> presets)
    {
        Index = index;
        Size = size;
        Presets = presets;
    }
}