using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;
using System.Collections.Generic;

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

    protected virtual void OnSceneGUI()
    {
        GunBuilder gunBuilder = Selection.activeGameObject.GetComponent<GunBuilder>();
        if (toolSelection.Selected == 0)
        {
            gunBuilder._Mode = GunBuilder.Mode.MoveCenter;
            OnSelection_MoveCenter();
        }
        if(toolSelection.Selected == 1)
        {
            gunBuilder._Mode = GunBuilder.Mode.MoveSurf;
            OnSelection_MoveSurf();
        }
        if(toolSelection.Selected == 2)
        {
            gunBuilder._Mode = GunBuilder.Mode.SetDirectionPoints;
            OnSelection_MoveDirectionPoints();
        }
        
    }
    private void OnSelection_MoveCenter()
    {
        Tools.current = Tool.Move;
    }

    private void OnSelection_MoveSurf()
    {


        GunBuilder gunBuilder = (GunBuilder)target;
        Tools.current = Tool.Custom;

        EditorGUI.BeginChangeCheck();
        Vector3 handlePosition = Handles.PositionHandle(gunBuilder.GetSurf().position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(gunBuilder, "Change Look At Target Position");
            gunBuilder.NewSurfPosition = handlePosition;
            gunBuilder.ApplySurfPosition();
        }
    }

    private void OnSelection_MoveDirectionPoints()
    {
        GunBuilder gunBuilder = (GunBuilder)target;
        Tools.current = Tool.Custom;

        if (dirPointSelection.Selected == 0)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 handlePosition = Handles.PositionHandle(gunBuilder.FirePosition, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(gunBuilder, "Change Fire Position");
                gunBuilder.FirePosition = handlePosition;
            }
        }
        else if (dirPointSelection.Selected == 1)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 handlePosition = Handles.PositionHandle(gunBuilder.JoinPosition, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(gunBuilder, "Change Join Position");
                gunBuilder.JoinPosition = handlePosition;
            }
        }




    }

    SelectionE toolSelection;
    SelectionE dirPointSelection;
    private void OnEnable()
    {
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

    public override void OnInspectorGUI()
    {
        toolSelection.OnGUI();
        GUIExtensions.Button("Apply Changes", OnApplyChanges);
        if(toolSelection.Selected == 2)
        {
            dirPointSelection.OnGUI();
        }
    }

    private void OnApplyChanges()
    {
        /*
         * pos = getSurfPos()
         * headAssetName = GetHeadAssetName()
         * gunAssetName = GetGunAssetName()
         * newAsset =  CreateAsset in headAsset.Folder
         *  where newAsset.name = headAssetName_PositionData.asset
         */
    }
}

