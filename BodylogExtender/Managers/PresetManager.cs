using System.Text.Json;
using BodylogExtender.presets;
using MelonLoader;
using MelonLoader.Utils;

namespace BodylogExtender.globals;

public abstract class PresetManager
{
    private static readonly string FilePath = MelonEnvironment.UserDataDirectory + "/FavoriteAvatars.json";
    
    private static PresetState _presetState = GetDefaultPresetState();

    private static PresetState GetDefaultPresetState()
    {
        return new PresetState();
    }

    public static void SetActivePreset(AvatarPreset preset)
    {
        _presetState.Presets[_presetState.Index] = preset;
    }

    public static AvatarPreset GetActivePreset()
    {
        return _presetState.Presets.TryGetValue(_presetState.Index, out var preset) ? preset : new AvatarPreset();
    }

    public static void ToNextPreset()
    {
        _presetState.Index = (_presetState.Index + 1) % _presetState.Presets.Count;
    }
    
    // Saving / Loading
    
    private static PresetState? LoadPresetState()
    {
        if (!File.Exists(FilePath)) return null;
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<PresetState>(json);
        }
        catch (Exception exception)
        {
            MelonLogger.Error("Failed to load presets from file!", exception);
            return null;
        }
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