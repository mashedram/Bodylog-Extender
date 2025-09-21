using System.Text.Json.Serialization;
using BodylogExtender.Util;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow.Warehouse;
using MelonLoader;

namespace BodylogExtender.AvatarData;

public class AvatarPreset
{
    private const string FallbackAvatarBarcode = "fa534c5a83ee4ec6bd641fec424c4142.Avatar.Strong";
    private const int RandomAvatarListSize = 16;
    private static readonly string[] RandomAvatarList = new string[RandomAvatarListSize];
    
    [JsonInclude] public readonly string[] Avatars;
    
    private static string GetRandomAvatarBarcode()
    {
        var index = UnityEngine.Random.RandomRangeInt(0, RandomAvatarListSize);
        return RandomAvatarList[index];
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
    
    public static void PopulateRandomAvatarList()
    {
        var crates = AssetWarehouse.Instance.GetCrates<AvatarCrate>();
        #if DEBUG
        MelonLogger.Msg($"Found {crates._size} avatars");
        #endif
        var stepSize = crates._size / RandomAvatarListSize;
        for (var i = 0; i < RandomAvatarListSize; i++)
        {
            RandomAvatarList[i] = crates._items[i * stepSize].Barcode.ID;
        }
    }
}