using System.Text.Json.Serialization;

namespace BodylogExtender.AvatarData;

public class PresetState
{
    [JsonInclude] 
    public uint Size = 4;

    [JsonInclude] public uint Index;
    
    [JsonInclude]
    public Dictionary<int, AvatarPreset> Presets = new();

    [JsonIgnore]
    public AvatarPreset ActivePreset
    {
        get => Presets[(int) Index];
        set => Presets[(int) Index] = value;
    }

    public AvatarPreset GetOrCreateActivePreset()
    {
        if (Presets.TryGetValue((int) Index, out var preset)) return preset;
        
        var newPreset = new AvatarPreset(new []
        {
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong",
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong",
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong",
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong",
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong",
            "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong"
        });
        Presets.Add((int) Index, newPreset);
        return newPreset;

    }
}