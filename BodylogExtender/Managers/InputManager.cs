using BoneLib;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;

namespace BodylogExtender.globals;

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
        return GetGrabbingController()._menuTap;
    }
}