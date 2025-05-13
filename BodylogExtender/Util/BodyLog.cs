using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Bonelab.SaveData;
using Il2CppSLZ.Marrow.SaveData;

namespace BodylogExtender.presets;

public class BodyLog
{
    public const int BodylogAvatarCount = 6;
    private readonly PullCordDevice _pullCordDevice;

    public BodyLog(PullCordDevice pullCordDevice)
    {
        _pullCordDevice = pullCordDevice;
    }
    
    public static AvatarPreset GetPreset()
    {
        var favorites = DataManager.ActiveSave._PlayerSettings_k__BackingField._FavoriteAvatars_k__BackingField;
        
        var preset = new AvatarPreset();
        for (var i = 0; i < favorites._size; i++)
        {
            preset.SetAvatar(i, favorites._items[i]);
        }
        
        return preset;
    }

    private static Il2CppSystem.Collections.Generic.List<string> GetAvatarsNativeList(AvatarPreset preset)
    {
        var nativeList = new Il2CppSystem.Collections.Generic.List<string>(6)
        {
            _size = 6,
        };

        for (var i = 0; i < BodylogAvatarCount; i++)
        {
            nativeList._items[i] = preset.Avatars[i];
        }
        
        return nativeList;
    }

    public void SetPreset(AvatarPreset preset)
    {
        var nativeAvatarList = GetAvatarsNativeList(preset);

        DataManager.ActiveSave._PlayerSettings_k__BackingField._FavoriteAvatars_k__BackingField = nativeAvatarList;
        DataManager.TrySaveActiveSave((SaveFlags)0);

        _pullCordDevice.LoadFavoriteAvatars();
        _pullCordDevice.UpdateAllPreviewMeshes();
        _pullCordDevice.SetPreviewMesh(_pullCordDevice.avatarIndex);  
    }
}