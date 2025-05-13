using BodylogExtender.Managers;
using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace BodylogExtender.Patches;

[HarmonyPatch(typeof(PullCordDevice), nameof(PullCordDevice.OnBallGripAttached))]
public class BodyLogOnGripAttachedPatch
{
    [HarmonyPrefix]
    private static bool Prefix(PullCordDevice __instance)
    {
        BodyLogManager.OnBodyLogGrabbed(__instance);
        return true;
    }
}