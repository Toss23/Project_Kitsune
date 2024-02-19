using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader
{
    private Dictionary<string, AsyncOperationHandle> _assets;

    public AssetLoader(IContext context)
    {
        context.RegisterAssetLoader(this);

        _assets = new Dictionary<string, AsyncOperationHandle>();
    }

    public async Task Load(string name)
    {
        AsyncOperationHandle handle;

        if (_assets.ContainsKey(name))
        {
            handle = _assets[name];
            await handle.Task;
        }
        else
        {
            handle = Addressables.LoadAssetAsync<GameObject>(name);
            await handle.Task;
            if (handle.Task.Status == TaskStatus.RanToCompletion)
            {
                _assets.Add(name, handle);
            }
        }
    }

    public AsyncOperationHandle Get(string name)
    {
        return _assets[name];
    }

    public void Release(string name)
    {
        if (_assets.ContainsKey(name))
        {
            AsyncOperationHandle handle = _assets[name];
            _assets.Remove(name);
            Addressables.Release(handle);
        }
    }

    public void ReleaseAll()
    {
        Dictionary<string, AsyncOperationHandle> assets = new Dictionary<string, AsyncOperationHandle>();
        foreach (KeyValuePair<string, AsyncOperationHandle> asset in _assets)
        {
            assets.Add(asset.Key, asset.Value);
        }

        foreach (KeyValuePair<string, AsyncOperationHandle> asset in assets)
        {
            Release(asset.Key);
        }
    }
}
