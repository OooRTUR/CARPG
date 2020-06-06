using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;

[CustomEditor(typeof(GunBuilder))]
public class GunBuilderEditor : Editor
{
    [MenuItem("Tools/Create Centered Gun From Gun Models Folder")]
    static void CreateCenteredGun()
    {
        string[] selectedGunGuids = Selection.assetGUIDs;
        if (selectedGunGuids.Length != 1)
        {
            Debug.LogWarning("You need to choose only one gun object");
            return;
        }

        var path = AssetDatabase.GUIDToAssetPath(selectedGunGuids.First());
        if (Path.Combine(Paths.prefabsPath, "Gun\\Models") != Path.GetDirectoryName(path))
        {
            Debug.LogWarning("This is not gun models path");
            return;
        }

        string gunModelName =  Path.GetFileNameWithoutExtension(path);

        GameObject gun = new GameObject(gunModelName);
        GameObject gunModelAsset =  (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        GameObject gunSurf =  GameObject.Instantiate(gunModelAsset,gun.transform);
        gunSurf.name = "Surf";
        gun.AddComponent<Gun>();
        Undo.RegisterCreatedObjectUndo(gun, "Gun Object Created");

        Selection.activeGameObject = gun;
    }

    
}

