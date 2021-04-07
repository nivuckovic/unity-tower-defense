using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AssetLoader
{
    public static void Instantiate(string address, Transform _parent = null)
    {
        // LoadAsset(address);
        Addressables.InstantiateAsync(address, parent : _parent);
    }

    // TODO: template func

    static void LoadAsset(string address)
    {
        Addressables.LoadAssetAsync<GameObject>(address).Completed += AssetLoaded;
    }

    static void AssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        // Addressables.InstantiateAsync(obj.)
    }
}
