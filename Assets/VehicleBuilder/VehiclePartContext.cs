using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;
using System.Linq;

public class VehiclePartContext
{
    public string assetTypeName;
    private string path;

    private GameObject prefab;
    private UnityEngine.Object asset;

    public VehiclePartContext(string path, Type assetType)
    {
        this.path = path;
        asset = CreateAssetIfNotExist(path, assetType);
    }

    private UnityEngine.Object CreateAssetIfNotExist(string path, Type assetType)
    {
        string assetPath = Path.ChangeExtension(this.path, "asset");
        UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, assetType);
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance(assetType);
            AssetDatabase.CreateAsset(asset, assetPath);
        }
        return asset;
    }

    public void AddToAsset(UnityEngine.Object data)
    {
        var assetPath = GetAssetPath();
        UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
        var res = assets.Where(asset => asset.name == data.name).ToList();
        if (res.Count < 1){
            AssetDatabase.AddObjectToAsset(data, assetPath);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(data));
        }
        
    }

    public UnityEngine.Object GetSubAsset(string subAssetName, Type subAssetType)
    {
        UnityEngine.Object res = null;
        var assetPath = GetAssetPath();
        UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
        var foundSubAssets = assets.Where(asset => asset.name == subAssetName).ToList();
        if (foundSubAssets.Count < 1)
        {
            res = ScriptableObject.CreateInstance(subAssetType);
            res.name = subAssetName;
            AssetDatabase.AddObjectToAsset(res, assetPath);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(res));
        }
        else
        {
            res = foundSubAssets[0];
        }
        return res;
    }
    
    public GameObject GetPrefab()
    {
        return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
    }
    public UnityEngine.Object GetAsset()
    {
        return (UnityEngine.Object)AssetDatabase.LoadAssetAtPath(GetAssetPath(), typeof(UnityEngine.Object));
    }
    public string GetAssetPath()
    {
        return Path.ChangeExtension(path, "asset");
    }
    public string GetPartTypeName()
    {
        return Path.GetFileNameWithoutExtension(path);
    }
    
}
