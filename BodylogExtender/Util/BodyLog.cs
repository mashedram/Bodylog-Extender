using BodylogExtender.AvatarData;
using BodylogExtender.Managers;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Bonelab.SaveData;
using Il2CppSLZ.Marrow.SaveData;
using UnityEngine;

namespace BodylogExtender.Util;

public class BodyLog
{
    public const int BodylogAvatarCount = 6;
    private readonly PullCordDevice _pullCordDevice;
    private readonly MeshRenderer _meshRenderer;
    public bool IsGrabbed = false;
    public bool IsColorDirty = false;

    public BodyLog(PullCordDevice pullCordDevice)
    {
        _pullCordDevice = pullCordDevice;

        _meshRenderer = pullCordDevice.previewMeshRenderer;
    }
    
    public static AvatarPreset GetPreset()
    {
        var favorites = DataManager.ActiveSave._PlayerSettings_k__BackingField._FavoriteAvatars_k__BackingField;

        var avatars = new string[BodylogAvatarCount];
        for (var i = 0; i < favorites._size; i++)
        {
            avatars[i] = favorites._items[i];
        }
        var preset = new AvatarPreset(avatars);
        
        return preset;
    }

    private static Il2CppSystem.Collections.Generic.List<string> GetAvatarsNativeList(AvatarPreset preset)
    {
        var nativeList = new Il2CppSystem.Collections.Generic.List<string>();

        for (var i = 0; i < BodylogAvatarCount; i++)
        {
            nativeList.Add(preset.Avatars[i]);
        }
        
        return nativeList;
    }

    public static void SetPreset(AvatarPreset preset, BodyLog? bodyLog = null)
    {
        var nativeAvatarList = GetAvatarsNativeList(preset);

        DataManager.ActiveSave._PlayerSettings_k__BackingField._FavoriteAvatars_k__BackingField = nativeAvatarList;
        DataManager.TrySaveActiveSave((SaveFlags)0);

        bodyLog?.SyncToGameObject();
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
        _meshRenderer.material.SetColor("_EmissionColor", color);
    }

    public bool IsValid()
    {
        return _pullCordDevice;
    }

    public void SyncToGameObject()
    {
        _pullCordDevice.LoadFavoriteAvatars();
        _pullCordDevice.UpdateAllPreviewMeshes();
        // Possible fix to the crate issue
        
        _pullCordDevice.lastAvatarIndex = (_pullCordDevice.avatarIndex + 1) % BodylogAvatarCount;
        _pullCordDevice.SetPreviewMesh(_pullCordDevice.avatarIndex);

        IsColorDirty = true;
    }
}