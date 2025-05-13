using System.Text.Json;
using BodylogExtender.AvatarData;
using MelonLoader;
using MelonLoader.Utils;

namespace BodylogExtender.Managers;

public abstract class PresetManager
{
    private static readonly string FilePath = MelonEnvironment.UserDataDirectory + "/FavoriteAvatars.json";

    private static int _index;
    private static Dictionary<int, AvatarPreset> _presets = new();

    #if DEBUG
    public static int GetIndex()
    {
        return _index;
    }
    #endif

    public static void SetActivePreset(AvatarPreset preset)
    {
        _presets[_index] = preset;
    }

    public static AvatarPreset GetActivePreset()
    {
        return _presets.TryGetValue(_index, out var preset) ? preset : new AvatarPreset();
    }

    public static void ToNextPreset()
    {
        _index = (_index + 1) % Preferences.GetPresetCount();
    }
    
    // Saving / Loading
    
    private static PresetSaveData? LoadPresetState()
    {
        if (!File.Exists(FilePath)) return null;
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<PresetSaveData>(json);
        }
        catch (Exception exception)
        {
            MelonLogger.Error("Failed to load presets from file!", exception);
            return null;
        }
    }

    private static void SavePresetState(PresetSaveData presetSaveData)
    {
        var content = JsonSerializer.Serialize(presetSaveData);
        File.WriteAllText(FilePath, content);
    }

    public static void LoadPresetManager()
    {
        var presetState = LoadPresetState();
        if (presetState == null) return;
        _index = presetState.Index;
        _presets = presetState.GetPresets();
    }
    
    public static void SavePresetManager()
    {
        SavePresetState(new PresetSaveData(_index, _presets));
    }
}