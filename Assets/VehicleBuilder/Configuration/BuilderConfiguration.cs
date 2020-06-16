using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace VehicleBuilder.Configuration
{
    public class BuilderConfiguration : ScriptableObject
    {
        private string editName = "VehicleEditor";
        private string bodyDefinition = "Body";
        private string headDefinition = "Head";
        private string gunDefinition = "Gun";

        public string EditName { get { return editName; } }
        public string PrefabsFolderPath { get { return $"Assets\\Prefabs"; } }

        [CustomFolder]
        public string Body { get { return bodyDefinition; } }

        [CustomFolder]
        public string Head { get { return headDefinition; } }

        [CustomFolder(CreateModelsFolderRequired = true)]
        public string Gun { get { return gunDefinition; } }

        public string[] GetPartGuids(string partTypeName)
        {
            return AssetDatabase.FindAssets($"{partTypeName}. t:prefab", new[] { $"{PrefabsFolderPath}\\{partTypeName}" });
        }

        public IEnumerable<string> GetPartPaths(string partTypeName)
        {
            List<string> res = new List<string>();
            string[] guids = GetPartGuids(partTypeName);
            return guids.Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        }

        public IEnumerable<GameObject> LoadAssets(string partTypeName)
        {
            return GetPartPaths(partTypeName)
                .Select(path => (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
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
         *          ->  head1.prefab
         *          ->  head1_data
         *              ->  head1_gun1.asset
         *              ->  head1_gun2.asset
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
}