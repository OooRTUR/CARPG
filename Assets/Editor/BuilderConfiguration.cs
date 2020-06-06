using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public class BuilderConfiguration : ScriptableObject
{
    private string homeFolderName = "VehicleBuilder";
    private string bodyDefinition = "Body";
    private string headDefinition = "Head";
    private string gunDefinition = "Gun";

    public string HomeFolderName { get { return homeFolderName; } }
    public string HomeFolderPath { get { return $"Assets\\{homeFolderName}"; } }
    public string PrefabsFolderPath { get { return $"{HomeFolderPath}\\Prefabs"; } }

    [CustomFolder]
    public string Body { get { return bodyDefinition; } }

    [CustomFolder]
    public string Head { get { return headDefinition; } }

    [CustomFolder(CreateModelsFolderRequired = true)]
    public string Gun { get { return gunDefinition; } }

    public string[] GetPartGuids(string partTypeName)
    {
        return AssetDatabase.FindAssets($"{partTypeName}.", new[] { $"{PrefabsFolderPath}\\{partTypeName}" });
    }
    public IEnumerable<string> GetPartPaths(string partTypeName)
    {
        List<string> res = new List<string>();
        string[] guids = GetPartGuids(partTypeName);
        return guids.Select(x => AssetDatabase.GUIDToAssetPath(x));
    }

    /*
     * VehicleBuilder
     *  ->  Prefabs
     *      ->  Gun
     *          ->  gun1.prefab
     *          ->  gun1.asset
     *          ->  gun2.prefab
     *          ->  gun2.asset
     *          ->  Models
     *      ->  Head
     *      ->  Body
     *  BuilderConfiguration.asset
     */



}

public class CustomFolderAttribute : Attribute
{
    private bool createModelsFolderRequired;
    public bool CreateModelsFolderRequired
    {
        get { return createModelsFolderRequired; }
        set { createModelsFolderRequired = true; }
    }
}