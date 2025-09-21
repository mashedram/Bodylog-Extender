using BodylogExtender.Managers;
using BodylogExtender.Util;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;
using LabFusion;
using LabFusion.Utilities;
using MelonLoader;
using UnityEngine;

namespace BodylogExtender.Managers;

public abstract class BodyLogManager
{
    private static readonly bool IsFusionLoaded = MelonBase.FindMelon(FusionMod.ModName, FusionMod.ModAuthor).Registered;
    private static BodyLog? _activeBodyLog;

    private static bool IsLocalPlayer(RigManager rig)
    {
        return !IsFusionLoaded || rig.IsLocalPlayer();
    }
    
    public static void OnBodyLogGrabbed(PullCordDevice pullCordDevice)
    {
        if (!IsLocalPlayer(pullCordDevice.rm)) return;
        #if DEBUG
            MelonLogger.Msg("pullCordDevice Grabbed: " + pullCordDevice.name);
        #endif

        if (_activeBodyLog == null || !_activeBodyLog.IsValid())
        {
            _activeBodyLog = new BodyLog(pullCordDevice);
            
            // Assign on first grab
            BodyLog.SetPreset(PresetManager.GetActivePreset(), _activeBodyLog);
            _activeBodyLog.IsGrabbed = true;
            return;
        }
        
        if (_activeBodyLog.IsGrabbed) return;
        
        _activeBodyLog.IsGrabbed = true;
        PresetManager.SetActivePreset(BodyLog.GetPreset());
    }

    public static void OnBodyLogReleased(PullCordDevice pullCordDevice)
    {
        if (!IsLocalPlayer(pullCordDevice.rm)) return;
        if (_activeBodyLog == null) return;
        if (!_activeBodyLog.IsValid()) return;
        if (!_activeBodyLog.IsGrabbed) return;
        #if DEBUG
            MelonLogger.Msg("pullCordDevice Released");
        #endif
        _activeBodyLog.IsGrabbed = false;
        
        PresetManager.SavePresetManager();
    }

    public static void Update()
    {
        if (_activeBodyLog == null) return;
        if (!_activeBodyLog.IsValid()) return;
        if (!_activeBodyLog.IsGrabbed) return;

        // Setting this directly after a mesh change makes the game internals overwrite the custom color immediatly.
        // So, we need to do this workaround
        if (_activeBodyLog.IsColorDirty)
        {
            _activeBodyLog.SetColor(PresetManager.GetPresetColor());
        }
        
        if (!InputManager.IsSwitchPresetButtonDown()) return;
        
        PresetManager.SetActivePreset(BodyLog.GetPreset());
        PresetManager.ToNextPreset();
        BodyLog.SetPreset(PresetManager.GetActivePreset(), _activeBodyLog);
        #if DEBUG
            MelonLogger.Msg("preset changed to: " + PresetManager.GetIndex());
        #endif
    }
}