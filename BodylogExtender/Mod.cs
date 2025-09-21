using BodylogExtender.AvatarData;
using BodylogExtender.Managers;
using BoneLib;
using MelonLoader;

[assembly: MelonInfo(typeof(BodylogExtender.Mod), "BodylogExtender", "1.0.0", "mash")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
namespace BodylogExtender;

public class Mod : MelonMod
{
    public static bool IsFusionLoaded = false;
    
    public override void OnInitializeMelon()
    {
        var harmony = new HarmonyLib.Harmony("BodylogExtender");
        harmony.PatchAll();

        IsFusionLoaded = FindMelon("LabFusion", "Lakatrazz") != null;
        
        PresetManager.LoadPresetManager();
        ModBoneMenu.CreateBoneMenu();
    }
    
    public override void OnUpdate()
    {
        BodyLogManager.Update();
    }
}