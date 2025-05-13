using System.Text.Json.Serialization;
using BodylogExtender.globals;
using UnityEngine.Rendering.Universal.LibTessDotNet;

namespace BodylogExtender.presets;

public class PresetState
{
    [JsonInclude]
    public int Index;
    [JsonInclude] 
    public Dictionary<int, AvatarPreset> Presets = new();
}