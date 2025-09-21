using BodylogExtender.Managers;
using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace BodylogExtender.Patches;

[HarmonyPatch(typeof(PullCordDevice))]
public class BodyLogPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(PullCordDevice.OnBallGripAttached))]
    private static bool OnBallGripAttachedPrefix(PullCordDevice __instance)
    {
        BodyLogManager.OnBodyLogGrabbed(__instance);
        return true;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(PullCordDevice.OnBallGripDetached))]
    private static bool OnBallGripDetachedPrefix(PullCordDevice __instance)
    {
        BodyLogManager.OnBodyLogReleased(__instance);
        return true;
    }
}