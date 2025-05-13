using System.Text.Json.Serialization;

namespace BodylogExtender.AvatarData;

public class PresetSaveData
{
    [JsonInclude]
    public int Index;
    [JsonInclude] 
    public Dictionary<int, string[]> Presets;

    public PresetSaveData()
    {
        Index = 0;
        Presets = new Dictionary<int, string[]>();
    }
    
    public PresetSaveData(int index, Dictionary<int, string[]> presets)
    {
        Index = index;
        Presets = presets;
    }

    public PresetSaveData(int index, Dictionary<int, AvatarPreset> presets)
    {
        Index = index;
        Presets = presets.ToDictionary(pair => pair.Key, pair => pair.Value.Avatars);
    }

    public Dictionary<int, AvatarPreset> GetPresets()
    {
        return Presets.ToDictionary(pair => pair.Key, pair => new AvatarPreset(pair.Value));
    }
}