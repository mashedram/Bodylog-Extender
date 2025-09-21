using System.Text.Json;
using System.Text.Json.Serialization;
using BodylogExtender.AvatarData;
using MelonLoader;
using MelonLoader.Utils;
using UnityEngine;

namespace BodylogExtender.Managers;

public static class PresetManager
{
    private static readonly string FilePath = MelonEnvironment.UserDataDirectory + "/FavoriteAvatars.json";

    private static PresetState _state = new();

    #if DEBUG
    public static uint GetIndex()
    {
        return _state.Index;
    }
    #endif

    public static void SetActivePreset(AvatarPreset preset)
    {
        _state.ActivePreset = preset;
    }

    public static Color GetPresetColor()
    {
        var hue = (1.0f / _state.Size) * _state.Index;
        return Color.HSVToRGB(hue, 1.0f, 1.0f);
    }

    public static AvatarPreset GetActivePreset()
    {
        return _state.GetOrCreateActivePreset();
    }

    public static void ToNextPreset()
    {
        _state.Index = (_state.Index + 1) % _state.Size;
    }

    public static uint GetPresetCount()
    {
        return _state.Size;
    }

    public static void SetPresetCount(uint count)
    {
        _state.Size = count;
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

    private static void SavePresetState(PresetState presetSaveData)
    {
        var content = JsonSerializer.Serialize(presetSaveData);
        File.WriteAllText(FilePath, content);
    }

    public static void LoadPresetManager()
    {
        var presetState = LoadPresetState();
        if (presetState == null) return;
        _state = presetState;
    }
    
    public static void SavePresetManager()
    {
        SavePresetState(_state);
    }
}