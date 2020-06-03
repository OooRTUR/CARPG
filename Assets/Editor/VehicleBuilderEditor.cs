using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.VersionControl;
using UnityEditor.UIElements;
using UnityEngine.Experimental.GlobalIllumination;

public class VehicleBuilderEditorWindow : EditorWindow
{
    static readonly string vehicleName = "VehicleEditor";
    VehicleBuilderTempData data = null;
    Type selectionInfo;
    GameObject body;
    GameObject head;
    GameObject gun;

    [MenuItem("Tools/Vehicle Builder Window")]
    static void Init()
    {
        VehicleBuilderEditorWindow window = (VehicleBuilderEditorWindow)EditorWindow.GetWindow(typeof(VehicleBuilderEditorWindow));
        window.Show();
        if (GameObject.Find(vehicleName) == null)
        {

        }
    }

    private void OnSelectionChange()
    {
        selectionInfo = GetSelectionInfo(Selection.activeGameObject);
    }
    private void OnDestroy()
    {
        DestroyVehicle();
    }
    private void OnEnable()
    {
        InitTempData();
        CreateVehicle();
        
    }
    

    void OnGUI()
    {
        GUILayout.Label("Body");
        body = (GameObject)EditorGUILayout.ObjectField(body, typeof(GameObject), false);
        GUILayout.Label("Head");
        head = (GameObject)EditorGUILayout.ObjectField(head, typeof(GameObject), false);
        GUILayout.Label("Gun");
        gun = (GameObject)EditorGUILayout.ObjectField(gun, typeof(GameObject), false);

        if (Selection.activeObject != null)
        {
            if(selectionInfo.Name == "Body")
            {
                OnBodySelected();
            }
        }
    }

    private void OnBodySelected()
    {
        
    }


    private void CreateVehicle()
    {
        if (GameObject.Find(vehicleName) != null) return;

        GameObject newVehicleObj = new GameObject(vehicleName);
        VehicleBuilder vehicle = newVehicleObj.AddComponent<VehicleBuilder>();
        vehicle.SetBody(body);
        vehicle.SetHead(head);
        vehicle.SetGun(gun);
        Selection.activeGameObject = newVehicleObj;
    }
    private void DestroyVehicle()
    {
        DestroyImmediate(GameObject.Find(vehicleName));
    }

    private void InitTempData()
    {
        if (!AssetDatabase.IsValidFolder("Assets/VehicleBuilderTempData"))
            AssetDatabase.CreateFolder("Assets", "VehicleBuilderTempData");

        string[] res = AssetDatabase.FindAssets(vehicleName, new[] { "Assets/VehicleBuilderTempData" });
        

        if (res.Length < 1)
        {
            data = ScriptableObject.CreateInstance<VehicleBuilderTempData>();
            AssetDatabase.CreateAsset(data, $"Assets/VehicleBuilderTempData/{vehicleName}.asset");
        }
        else
        {
             data = (VehicleBuilderTempData)AssetDatabase.LoadAssetAtPath($"Assets/VehicleBuilderTempData/{vehicleName}.asset", typeof(VehicleBuilderTempData));
        }

        body = data.body;
        head = data.head;
        gun = data.gun;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }    

    private Type GetSelectionInfo(GameObject selection)
    {
        if (selection == null) return null;

        if (selection.GetComponent<VehicleBuilder>() != null) return typeof(VehicleBuilder);

        if (selection.transform.parent != null &&
            selection.transform.parent.GetComponent<VehicleBuilder>() != null)
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
    public GameObject body;
    public GameObject head;
    public GameObject gun;


    private void OnEnable()
    {
        if(body == null)
        {
            body = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Resources/Prefabs/Body/Body.Tank.prefab", typeof(GameObject));
        }
        if (head == null)
        {
            head = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Resources/Prefabs/Head/Head.Head.prefab", typeof(GameObject));
        }
        if (gun == null)
        {
            gun = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Resources/Prefabs/Gun/Gun.32mm.prefab", typeof(GameObject));
        }
    }
}