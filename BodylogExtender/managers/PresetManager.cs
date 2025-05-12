using System.Text.Json;
using BodylogExtender.presets;
using MelonLoader.Utils;

namespace BodylogExtender.globals;

public abstract class PresetManager
{
    private static readonly string FilePath = MelonEnvironment.UserDataDirectory + "/FavoriteAvatars.json";
    
    private static PresetState _presetState = GetDefaultPresetState();

    private static PresetState GetDefaultPresetState()
    {
        const int size = 4;
        var presets = new List<AvatarPreset>();
        for (var i = 0; i < size; i++)
        {
            presets.Add(new AvatarPreset());
        }
        return new PresetState(0, size, presets);
    }

    public static void SetActivePreset(AvatarPreset preset)
    {
        _presetState.Presets[_presetState.Index] = preset;
    }

    public static AvatarPreset GetActivePreset()
    {
        return _presetState.Presets[_presetState.Index];
    }

    public static int GetIndex()
    {
        return _presetState.Index;
    }

    public static void ToNextPreset()
    {
        _presetState.Index = (_presetState.Index + 1) % _presetState.Presets.Count;
    }
    
    // Saving / Loading
    
    private static PresetState? LoadPresetState()
    {
        if (!File.Exists(FilePath)) return null;
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<PresetState>(json);
    }

    private static void SavePresetState(PresetState presetState)
    {
        var content = JsonSerializer.Serialize(presetState);
        File.WriteAllText(FilePath, content);
    }

    public static void LoadPresetManager()
    {
        var presetState = LoadPresetState();
        if (presetState == null) return;
        _presetState = presetState;
    }
    
    public static void SavePresetManager()
    {
        SavePresetState(_presetState);
    }
}