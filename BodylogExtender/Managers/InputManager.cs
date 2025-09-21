using BoneLib;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;
using UnityEngine;

namespace BodylogExtender.Managers;

public abstract class InputManager
{
    private static BaseController GetGrabbingController()
    {
        return PlayerRefs._Instance_k__BackingField._bodyVitals.bodyLogFlipped ? 
            Player.RightController : 
            Player.LeftController;
    }

    public static bool IsSwitchPresetButtonDown()
    {
        #if DEBUG
        // I Debug in flatplayer
        // Fight me
        if (Input.GetKeyDown(KeyCode.G))
            return true;
        #endif
        return GetGrabbingController()._menuTap;
    }
}