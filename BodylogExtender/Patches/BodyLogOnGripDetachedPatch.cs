using BodylogExtender.Managers;
using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace BodylogExtender.Patches;

[HarmonyPatch(typeof(PullCordDevice), nameof(PullCordDevice.OnBallGripDetached))]
public class BodyLogOnGripDetachedPatch
{
    [HarmonyPrefix]
    private static bool Prefix(PullCordDevice __instance)
    {
        BodyLogManager.OnBodyLogReleased(__instance);
        return true;
    }
}