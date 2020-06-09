using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class FileStructureUtile
{
    public static void CreateFolderStructure()
    {
        var config = ScriptableObject.CreateInstance<BuilderConfiguration>();
        if (!AssetDatabase.IsValidFolder(config.PrefabsFolderPath))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        Type configtype = typeof(BuilderConfiguration);
        IEnumerable<PropertyInfo> props = configtype.GetProperties().
            Where(prop => Attribute.IsDefined(prop, typeof(CustomFolderAttribute)));

        foreach (PropertyInfo propInfo in props)
        {
            var customFolderName = (string)propInfo.GetValue(config);
            var customFolderPath = $"{config.PrefabsFolderPath}\\{customFolderName}";
            if (!AssetDatabase.IsValidFolder(customFolderPath))
                AssetDatabase.CreateFolder(config.PrefabsFolderPath, customFolderName);

            var customFolderAttribute = (CustomFolderAttribute)propInfo.GetCustomAttribute(typeof(CustomFolderAttribute));
            if (customFolderAttribute.CreateModelsFolderRequired && !AssetDatabase.IsValidFolder(customFolderPath))
                AssetDatabase.CreateFolder(customFolderPath, "Models");
        }
    }
}
