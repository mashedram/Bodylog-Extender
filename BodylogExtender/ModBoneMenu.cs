using BoneLib.BoneMenu;
using UnityEngine;

namespace BodylogExtender;

public class ModBoneMenu
{
    private Page _root;
    
    public ModBoneMenu(Action<int> callback)
    {
        _root = Page.Root.CreatePage("BodyLog Extender", Color.magenta);
        _root.CreateInt("Preset Count", Color.magenta, 4, 1, 0, 10, callback);
    }
}