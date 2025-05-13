using BodylogExtender.globals;
using BoneLib;
using MelonLoader;

[assembly: MelonInfo(typeof(BodylogExtender.Mod), "BodylogExtender", "1.0.0", "mash")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
namespace BodylogExtender;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        var harmony = new HarmonyLib.Harmony("BodylogExtender");
        harmony.PatchAll();
        
        Preferences.Load();
        PresetManager.LoadPresetManager();
        Hooking.OnLevelLoaded += OnLevelLoaded;
        Hooking.OnLevelUnloaded += OnLevelUnloaded;
        ModBoneMenu.CreateBoneMenu();
    }

    public override void OnDeinitializeMelon()
    {
        PresetManager.SavePresetManager();
        Preferences.Save();
    }

    private static void OnLevelUnloaded() {
        PresetManager.SavePresetManager();
    }

    private static void OnLevelLoaded(LevelInfo obj)
    {
        PresetManager.LoadPresetManager();
    }

    public override void OnUpdate()
    {
        BodyLogManager.Update();
    }
}