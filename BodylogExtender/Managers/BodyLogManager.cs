﻿using BodylogExtender.Managers;
using BodylogExtender.Util;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;
using LabFusion;
using LabFusion.Utilities;
using MelonLoader;

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
        _activeBodyLog = new BodyLog(pullCordDevice);
        PresetManager.SetActivePreset(BodyLog.GetPreset());
    }

    public static void OnBodyLogReleased(PullCordDevice pullCordDevice)
    {
        if (!IsLocalPlayer(pullCordDevice.rm)) return;
        #if DEBUG
            MelonLogger.Msg("pullCordDevice Released");
        #endif
        _activeBodyLog = null;
    }

    public static void Update()
    {
        if (_activeBodyLog == null) return;
        if (!InputManager.IsSwitchPresetButtonDown()) return;
        
        PresetManager.SetActivePreset(BodyLog.GetPreset());
        PresetManager.ToNextPreset();
        _activeBodyLog.SetPreset(PresetManager.GetActivePreset());
        #if DEBUG
            MelonLogger.Msg("preset changed to: " + PresetManager.GetIndex());
        #endif
    }
}