using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;

[CustomEditor(typeof(GunBuilder))]
public class GunBuilderEditor : Editor
{
    SelectionE toolSelection;
    SelectionE dirPointSelection;

    GunBuilder builder;

    /// <summary>
    /// Create gun object with empty parent
    /// </summary>
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
        var config = (BuilderConfiguration)ScriptableObject.CreateInstance(typeof(BuilderConfiguration));

        if (Path.Combine(config.PrefabsFolderPath, "Gun\\Models") != Path.GetDirectoryName(path))
        {
            Debug.LogWarning("This is not gun models path");
            return;
        }

        string gunModelName =  Path.GetFileNameWithoutExtension(path);

        var gun = new GameObject(gunModelName);
        var gunModelAsset =  (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        var gunSurf =  GameObject.Instantiate(gunModelAsset,gun.transform);
        gunSurf.name = "Surf";
        gun.AddComponent<Gun>();
        Undo.RegisterCreatedObjectUndo(gun, "Gun Object Created");

        Selection.activeGameObject = gun;
    }

    private void OnEnable()
    {
        builder = Selection.activeGameObject.GetComponent<GunBuilder>();
        toolSelection = new SelectionE(new string[]{
            "Move Center",
            "Move Surf",
            "Move Direction Points"
        });
        dirPointSelection = new SelectionE(new string[]
        {
            "Fire Point",
            "Join Point"
        });
    }

    protected virtual void OnSceneGUI()
    {
        if (toolSelection.Selected == 0)
        {
            builder._Mode = GunBuilder.Mode.MoveCenter;
            OnToolSelection_MoveCenter();
        }
        if(toolSelection.Selected == 1)
        {
            builder._Mode = GunBuilder.Mode.MoveSurf;
            OnToolSelection_MoveSurf();
        }
        if(toolSelection.Selected == 2)
        {
            builder._Mode = GunBuilder.Mode.SetDirectionPoints;
            OnToolSelection_MoveDirectionPoints();
        }
        
    }

    public override void OnInspectorGUI()
    {
        toolSelection.OnGUI();
        if (toolSelection.Selected == 2)
        {
            dirPointSelection.OnGUI();
        }
        GUIExtensions.Button("Apply Changes", OnApplyButtonPressed);
    }

    #region OnToolSelection 
    private void OnToolSelection_MoveCenter()
    {
        Tools.current = Tool.Custom;
        GUIExtensions.PositionHandle(builder.gunData, typeof(GunData), "CenterPosition");
    }

    private void OnToolSelection_MoveSurf()
    {
        Tools.current = Tool.Move;
        
    }

    private void OnToolSelection_MoveDirectionPoints()
    {
        var gunBuilder = (GunBuilder)target;
        Tools.current = Tool.Custom;

        if (dirPointSelection.Selected == 0)
        {
            GUIExtensions.PositionHandle(gunBuilder.gunData, typeof(GunData), "FirePoint");
        }
        else if (dirPointSelection.Selected == 1)
        {
            GUIExtensions.PositionHandle(gunBuilder.gunData, typeof(GunData), "JoinPoint");
        }




    }
    #endregion

    private void OnApplyButtonPressed()
    {
        //builder.gunData.
        
        /*
         * SAVE DATA
         * pos = getSurfPos()
         * headAssetName = GetHeadAssetName()
         * gunAssetName = GetGunAssetName()
         * newAsset =  CreateAsset in headAsset.Folder
         *  where newAsset.name = headAssetName_PositionData.asset
         */
    }

    //private void InitTempData()
    //{


    //    if (res.Length < 1)
    //    {
    //        data = ScriptableObject.CreateInstance<VehicleBuilderTempData>();
    //        AssetDatabase.CreateAsset(data, $"Assets/VehicleBuilderTempData/{vehicleName}.asset");
    //    }
    //    else
    //    {
    //        data = (VehicleBuilderTempData)AssetDatabase.LoadAssetAtPath($"Assets/VehicleBuilderTempData/{vehicleName}.asset", typeof(VehicleBuilderTempData));
    //    }
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //}
}

