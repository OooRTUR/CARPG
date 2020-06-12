using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

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
    
    public GameObject GetPrefab()
    {
        return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
    }
    public UnityEngine.Object GetAsset()
    {
        string assetPath = Path.ChangeExtension(this.path,"asset");
        return (UnityEngine.Object)AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));
    }
    public string GetPartTypeName()
    {
        return Path.GetFileNameWithoutExtension(path);
    }
    
}
