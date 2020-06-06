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
        bodySelection.SelectedChanged += BodySelection_SelectedChanged;

        headSelection = new PartSelectionGUI(data.HeadPaths);
        headSelection.SelectedChanged += HeadSelection_SelectedChanged;

        gunSelection = new PartSelectionGUI(data.GunPaths);
        gunSelection.SelectedChanged += GunSelection_SelectedChanged;

        toolSelection = new ToolSelectionGUI();

        CreateVehicle();
        
    }

    private void GunSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetGun((GameObject)AssetDatabase.LoadAssetAtPath(gunSelection.GetSelectedPath(), typeof(GameObject)));
    }

    private void HeadSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetHead((GameObject)AssetDatabase.LoadAssetAtPath(headSelection.GetSelectedPath(), typeof(GameObject)));
    }

    private void BodySelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetBody((GameObject)AssetDatabase.LoadAssetAtPath(bodySelection.GetSelectedPath(), typeof(GameObject)));
    }

    void OnGUI()
    {
        if (Selection.activeObject != null)
        {
            bodySelection.OnGUI();
            headSelection.OnGUI();
            gunSelection.OnGUI();
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
        BodySelection_SelectedChanged(null, null);
        HeadSelection_SelectedChanged(null, null);
        GunSelection_SelectedChanged(null, null);
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

    public event EventHandler SelectedChanged;
    

    int Selected
    {
        get { return selected; }
        set
        {
            if (selected != value)
            {
                selected = value;
                OnSelectedChanged();
            }
        }
    }
    public string SelectedPath { get; private set; }
    private void OnSelectedChanged()
    {
        SelectedPath = paths[Selected];
        SelectedChanged?.Invoke(this, new EventArgs());
    }

    public string GetSelectedPath()
    {
        return paths[selected];
    }

    public PartSelectionGUI(IEnumerable<string> paths)
    {
        this.paths = paths.ToArray();
        options = paths.Select(path => Path.GetFileName(path)).ToArray();
    }

    public void OnGUI()
    {
        Selected = EditorGUILayout.Popup(Selected, options);
    }
}
public class ToolSelectionGUI
{
    public int Selected { get; private set; }
    string[] options = new string[] { "None", "Head", "Gun" };
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
        else if (Selected == 2)
        {
            Selection.activeGameObject = builder.GetGun().gameObject;
        }
    }
}



public class VehicleBuilderTempData : ScriptableObject
{
    public static string[] GetPartGuids(string partTypeName)
    {
        return AssetDatabase.FindAssets($"{partTypeName}.", new[] { $"{Paths.prefabsPath}/{partTypeName}" });
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

    private Dictionary<string, string> curretPaths;

    public string GetPath(string partTypeName)
    {
        return curretPaths[partTypeName];
    }
    public void SetPath(string partTypeName, string path)
    {
        curretPaths[partTypeName] = path;
    }

    private void OnEnable()
    {
        curretPaths = new Dictionary<string, string>() {
            {"Body","" },
            {"Head","" },
            {"Gun","" }
        };

        bodyPaths = GetPartPaths("Body");
        headPaths = GetPartPaths("Head");
        gunPaths = GetPartPaths("Gun");        

        if (String.IsNullOrEmpty(curretPaths["Body"]))
        {
            curretPaths["Body"] = bodyPaths.First();
        }
        if (String.IsNullOrEmpty(curretPaths["Head"]))
        {
            curretPaths["Head"] = headPaths.First();
        }
        if (String.IsNullOrEmpty(curretPaths["Gun"]))
        {
            curretPaths["Gun"] = gunPaths.First();
        }
    }
}