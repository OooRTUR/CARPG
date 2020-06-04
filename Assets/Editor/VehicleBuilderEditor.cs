using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.IO;

public class VehicleBuilderEditorWindow : EditorWindow
{
    static readonly string vehicleName = "VehicleEditor";
    VehicleBuilderTempData data = null;
    Type selectionInfo;
    GameObject body;
    GameObject head;
    GameObject gun;

    private PartSelectionGUI bodySelection;
    private PartSelectionGUI headSelection;
    private PartSelectionGUI gunSelection;
    private ToolSelectionGUI toolSelection;


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
        DeleteTempData();
    }
    private void OnEnable()
    {
        InitTempData();
        bodySelection = new PartSelectionGUI(data.BodyPaths);
        headSelection = new PartSelectionGUI(data.HeadPaths);
        gunSelection = new PartSelectionGUI(data.GunPaths);
        toolSelection = new ToolSelectionGUI();

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
            bodySelection.OnGUI();
            GUIExtensions.Button("Apply", 
                delegate { builder.SetBody((GameObject)AssetDatabase.LoadAssetAtPath(bodySelection.GetSelectedPath(), typeof(GameObject))); });

            headSelection.OnGUI();
            GUIExtensions.Button("Aply",
                delegate { builder.SetHead((GameObject)AssetDatabase.LoadAssetAtPath(headSelection.GetSelectedPath(), typeof(GameObject))); });

            gunSelection.OnGUI();
            GUIExtensions.Button("Apply",
                delegate { builder.SetGun((GameObject)AssetDatabase.LoadAssetAtPath(gunSelection.GetSelectedPath(), typeof(GameObject))); });

            toolSelection.OnGUI(builder);


            if (selectionInfo.Name == "Body")
            {
                OnBodySelected();
            }
        }
        else
        {

        }
    }

    private void OnBodySelected()
    {
        
    }

    VehicleBuilder builder;
    private void CreateVehicle()
    {
        if (GameObject.Find(vehicleName) != null) return;

        GameObject newVehicleObj = new GameObject(vehicleName);
        builder = newVehicleObj.AddComponent<VehicleBuilder>();
        builder.SetBody(body);
        builder.SetHead(head);
        builder.SetGun(gun);
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
    private void DeleteTempData()
    {
        AssetDatabase.DeleteAsset($"Assets/VehicleBuilderTempData/{vehicleName}.asset");
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

public class PartSelectionGUI
{
    int selected = 0;
    string[] options;
    string[] paths;

    int Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            OnSelectedChanged();
        }
    }
    private void OnSelectedChanged()
    {

        //Debug.Log($"{options[selected]} {paths[selected]}");
    }

    public string GetSelectedPath()
    {
        return paths[selected];
    }

    public PartSelectionGUI(IEnumerable<string> paths)
    {
        this.paths = paths.ToArray();
        options = paths.Select(x => Path.GetFileName(x)).ToArray();
    }

    public void OnGUI()
    {
        Selected = EditorGUILayout.Popup(Selected, options);
    }
}
public class ToolSelectionGUI
{
    public int Selected { get; private set; }
    string[] options = new string[] { "None", "Head" };
    public void OnGUI(VehicleBuilder builder)
    {
        Selected = EditorGUILayout.Popup(Selected, options);
        if(Selected == 0)
        {
            Selection.activeGameObject = builder.gameObject;
        }
        else if (Selected == 1)
        {
            Selection.activeGameObject = builder.GetHead().gameObject;
        }
    }
}

public class GUIExtensions
{
    public static void Button(string label, Action action)
    {
        if (GUILayout.Button(label))
        {
            action.Invoke();
        }
    }
}

public class VehicleBuilderTempData : ScriptableObject
{
    public GameObject body;
    public GameObject head;
    public GameObject gun;


    private static string prefabsPath = "Assets/Resources/Prefabs";

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

    private IEnumerable<string> bodyPaths;
    private IEnumerable<string> headPaths;
    private IEnumerable<string> gunPaths;

    public IEnumerable<string> BodyPaths
    {
        get { return bodyPaths; }
    }
    public IEnumerable<string> HeadPaths
    {
        get { return headPaths; }
    }
    public IEnumerable<string> GunPaths
    {
        get { return gunPaths; }
    }

    private void OnEnable()
    {
        bodyPaths = GetPartPaths("Body");
        headPaths = GetPartPaths("Head");
        gunPaths = GetPartPaths("Gun");        

        if (body == null)
        {
            body = (GameObject)AssetDatabase.LoadAssetAtPath(bodyPaths.First(), typeof(GameObject));
        }
        if (head == null)
        {
            head = (GameObject)AssetDatabase.LoadAssetAtPath(headPaths.First(), typeof(GameObject));
        }
        if (gun == null)
        {
            gun = (GameObject)AssetDatabase.LoadAssetAtPath(gunPaths.First(), typeof(GameObject));
        }
    }
}