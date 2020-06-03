using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.VersionControl;

public class VehicleBuilderEditorWindow : EditorWindow
{
    [MenuItem("Tools/Vehicle Builder Window")]
    static void Init()
    {
        VehicleBuilderEditorWindow window = (VehicleBuilderEditorWindow)EditorWindow.GetWindow(typeof(VehicleBuilderEditorWindow));
        window.Show();       
    }

    Type selectionInfo;

    public void OnSelectionChange()
    {
        selectionInfo = GetSelectionInfo(Selection.activeGameObject);
        Debug.Log(selectionInfo);
    }

    string vehicleName = "New Vehicle";
    string bodyTypeName = "Muscle";
    string headTypeName = "Head";
    string gunTypeName = "32mm";
    void OnGUI()
    {
        vehicleName = GUILayout.TextField(vehicleName);
        bodyTypeName = GUILayout.TextField(bodyTypeName);
        headTypeName = GUILayout.TextField(headTypeName);
        gunTypeName = GUILayout.TextField(gunTypeName);


        if (GetSelectionInfo(Selection.activeGameObject) == null)
        {
            if (GUILayout.Button("Create Vehicle"))
            {
                GameObject newVehicleObj = new GameObject(vehicleName);
                Vehicle vehicle = newVehicleObj.AddComponent<Vehicle>();
                vehicle.SetBody(bodyTypeName);
                vehicle.SetHead(headTypeName);
                vehicle.SetGun(gunTypeName);
                Selection.activeGameObject = newVehicleObj;

                Debug.Log(vehicle.GetBody().name);
                Debug.Log(vehicle.GetHead().name);
                Debug.Log(vehicle.GetGun().name);

                CreateTempData();
            }
        }
        else
        {
            if(GUILayout.Button("Change part"))
            {

            }
        }
    }

    private void CreateTempData()
    {
        string[] res = AssetDatabase.FindAssets(vehicleName, new[] { "Assets/VehicleBuilderTempData" });

        if (res.Length < 1)
        {
            VehicleBuilderTempData data = ScriptableObject.CreateInstance<VehicleBuilderTempData>();
            data.bodyTypeName = bodyTypeName;
            data.headTypeName = headTypeName;
            data.gunTypeName = gunTypeName;
            if (!AssetDatabase.IsValidFolder("Assets/VehicleBuilderTempData"))
                AssetDatabase.CreateFolder("Assets", "VehicleBuilderTempData");
            AssetDatabase.CreateAsset(data, $"Assets/VehicleBuilderTempData/{vehicleName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        else
        {
            throw new System.Exception("There is asset with such name: " + vehicleName);
        }
    }

    private Type GetSelectionInfo(GameObject selection)
    {
        if (selection == null) return null;

        if (selection.GetComponent<Vehicle>() != null) return typeof(Vehicle);

        if (selection.transform.parent != null &&
            selection.transform.parent.GetComponent<Vehicle>() != null)
        {
            //This is tempory solution
            if (selection.GetComponent<Body>() != null) return typeof(Body);
            else if (selection.GetComponent<Head>() != null) return typeof(Head);
            else if (selection.GetComponent<Gun>() != null) return typeof(Gun);
        }

        return null;        
    }
}

public class VehicleBuilderTempData : ScriptableObject
{
    public string bodyTypeName;
    public string headTypeName;
    public string gunTypeName;
}