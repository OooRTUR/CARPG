using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace VehicleBuilder.Configuration
{
    public class FileStructureUtile
    {
        public static void CreateFolderStructure()
        {
            var config = ScriptableObject.CreateInstance<BuilderConfiguration>();
            if (!AssetDatabase.IsValidFolder(config.PrefabsFolderPath))
                AssetDatabase.CreateFolder("Assets", "Prefabs");

            Type configtype = typeof(BuilderConfiguration);
            IEnumerable<PropertyInfo> customFolderProps = configtype.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(CustomFolderAttribute)));

            foreach (PropertyInfo customFolderPropInfo in customFolderProps)
            {
                var customFolderName = (string)customFolderPropInfo.GetValue(config);
                var customFolderPath = $"{config.PrefabsFolderPath}\\{customFolderName}";
                if (!AssetDatabase.IsValidFolder(customFolderPath))
                    AssetDatabase.CreateFolder(config.PrefabsFolderPath, customFolderName);

                var customFolderAttribute = (CustomFolderAttribute)customFolderPropInfo.GetCustomAttribute(typeof(CustomFolderAttribute));
                if (customFolderAttribute.CreateModelsFolderRequired && !AssetDatabase.IsValidFolder(customFolderPath))
                    AssetDatabase.CreateFolder(customFolderPath, "Models");
            }
        }
    }
}
