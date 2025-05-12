using BodylogExtender.globals;
using HarmonyLib;
using Il2CppSLZ.Bonelab;
using MelonLoader;

namespace BodylogExtender.patches;

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