using BoneLib.BoneMenu;
using UnityEngine;

namespace BodylogExtender;

public abstract class ModBoneMenu
{
    public static void CreateBoneMenu()
    {
        var root = Page.Root.CreatePage("BodyLog Extender", Color.magenta);
        root.CreateInt("Preset Count", Color.magenta, Preferences.GetPresetCount(), 1, 0, 10, Preferences.SetPresetCount);
        root.CreateFunction("Force Save", Color.green, Preferences.Save);
    }
}