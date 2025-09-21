using BodylogExtender.AvatarData;
using BodylogExtender.Managers;
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
        
        PresetManager.LoadPresetManager();
        ModBoneMenu.CreateBoneMenu();
    }
    
    public override void OnUpdate()
    {
        BodyLogManager.Update();
    }
}