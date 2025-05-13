using BodylogExtender.globals;
using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace BodylogExtender.patches;

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