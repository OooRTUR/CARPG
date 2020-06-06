using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Paths
{
    public static string prefabsPath = "Assets\\Resources\\Prefabs";

    public static void CreateFoldersStructure()
    {

    }

    public static string[] GetPartGuids(string partTypeName)
    {
        return AssetDatabase.FindAssets($"{partTypeName}.", new[] { $"{prefabsPath}/{partTypeName}" });
    }
    public static IEnumerable<string> GetPartPaths(string partTypeName)
    {
        List<string> res = new List<string>();
        string[] guids = GetPartGuids(partTypeName);
        return guids.Select(x => AssetDatabase.GUIDToAssetPath(x));
    }
}