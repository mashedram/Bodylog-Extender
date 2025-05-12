using System.Text.Json.Serialization;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Bonelab.SaveData;
using Il2CppSLZ.Marrow.SaveData;
using MelonLoader;
using UnityEngine.InputSystem;

namespace BodylogExtender.presets;

public class AvatarPreset
{
    private const string FallbackAvatarBarcode = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong";
    [JsonInclude] public readonly string[] Avatars;

    public AvatarPreset()
    {
        Avatars = new string[BodyLog.BodylogAvatarCount];
        for (var i = 0; i < BodyLog.BodylogAvatarCount; i++)
        {
            Avatars[i] = FallbackAvatarBarcode;
        }
    }

    public AvatarPreset(string[] avatars)
    {
        if (avatars.Length != BodyLog.BodylogAvatarCount) 
            throw new Exception("Avatar list must have the same number of avatars.");
        
        Avatars = avatars;
    }
    
    public void SetAvatar(int index, string barcode)
    {
        if (index < 0 || index >= Avatars.Length)
        {
            MelonLogger.Error("Avatar Index out of range: ", index);
            return;
        }
        
        Avatars[index] = barcode;
    }
}