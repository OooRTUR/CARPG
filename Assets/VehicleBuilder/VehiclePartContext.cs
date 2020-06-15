using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;
using System.Linq;


/// <summary>
/// Class to send part asset data between editor and Builder Monobehaviour classes.
/// Intended to avoid hard linking between VehicleBuilder classes
/// </summary>
public class VehiclePartContext
{
    private string path;

    public VehiclePartContext(string path, Type assetType)
    {
        this.path = path;
        CreateAssetIfNotExist(path, assetType);
    }

    /// <summary>
    /// create file with .asset extension to store addition part Build data
    /// </summary>
    private void CreateAssetIfNotExist(string path, Type assetType)
    {
        string assetPath = Path.ChangeExtension(this.path, "asset");
        UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, assetType);
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance(assetType);
            AssetDatabase.CreateAsset(asset, assetPath);
        }
    }

    /// <summary>
    /// Method supposed to work with complex assets.
    /// Create subbasset if subbasset doesnt exist at GetAssetPath(). 
    /// returns SubAsset at GetAssetPath().
    /// </summary>
    public UnityEngine.Object GetSubAssetDirectly(string subAssetName, Type subAssetType)
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

    /// <summary>
    /// Get link to source prefab
    /// </summary>
    public GameObject GetPrefabDirectly()
    {
        return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
    }

    /// <summary>
    /// Get link to source asset
    /// </summary>
    public UnityEngine.Object GetAssetDirectly()
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
