using BodylogExtender.Managers;
using BoneLib.BoneMenu;
using UnityEngine;

namespace BodylogExtender;

public abstract class ModBoneMenu
{
    public static void CreateBoneMenu()
    {
        var root = Page.Root.CreatePage("BodyLog Extender", Color.magenta);
        root.CreateInt("Preset Count", Color.magenta, (int) PresetManager.GetPresetCount(), 1, 0, 10, value => PresetManager.SetPresetCount((uint) value));
        root.CreateFunction("Force Save", Color.green, PresetManager.SavePresetManager);
    }
}