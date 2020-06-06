using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.IO;
using System.Reflection;

public class VehicleBuilderEditorWindow : EditorWindow
{
    public static VehicleBuilderEditorWindow Instance
    {
        get { return GetWindow<VehicleBuilderEditorWindow>(); }
    }

    static readonly string vehicleName = "VehicleEditor";
    Type selectionInfo;
    GameObject selectedObject;

    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set
        {
            if (selectedObject != value)
            {
                selectedObject = value;
                OnSelectedObjectChanged();
            }
        }
    }

    private void OnSelectedObjectChanged()
    {

    }

    private PartSelectionGUI bodySelection;
    private PartSelectionGUI headSelection;
    private PartSelectionGUI gunSelection;
    private ToolSelectionGUI toolSelection;


    [MenuItem("Tools/Vehicle Builder Window")]
    static void Init()
    {
        VehicleBuilderEditorWindow window = (VehicleBuilderEditorWindow)EditorWindow.GetWindow(typeof(VehicleBuilderEditorWindow));
        window.Show();
    }

    private void OnSelectionChange()
    {
        selectionInfo = GetSelectionInfo(Selection.activeGameObject);
    }
    private void OnDestroy()
    {
        DestroyVehicle();
        //DeleteTempData();
    }
    private void OnEnable()
    {
        //InitTempData();
        BuilderConfiguration config =  CreateFolderStructure();
        bodySelection = new PartSelectionGUI(config.GetPartPaths("Body"));
        bodySelection.SelectedChanged += BodySelection_SelectedChanged;

        headSelection = new PartSelectionGUI(config.GetPartPaths("Head"));
        headSelection.SelectedChanged += HeadSelection_SelectedChanged;

        gunSelection = new PartSelectionGUI(config.GetPartPaths("Gun"));
        gunSelection.SelectedChanged += GunSelection_SelectedChanged;

        //toolSelection = new ToolSelectionGUI();

        CreateVehicle();

        Selection.activeObject = builder.gameObject;
        
    }
    private void BodySelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetBody((GameObject)AssetDatabase.LoadAssetAtPath(bodySelection.GetSelectedPath(), typeof(GameObject)));
        Selection.activeGameObject = builder.GetBody().gameObject;
    }

    private void HeadSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetHead((GameObject)AssetDatabase.LoadAssetAtPath(headSelection.GetSelectedPath(), typeof(GameObject)));
        Selection.activeGameObject = builder.GetHead().gameObject;
    }

    private void GunSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetGun((GameObject)AssetDatabase.LoadAssetAtPath(gunSelection.GetSelectedPath(), typeof(GameObject)));
        Selection.activeGameObject = builder.GetGun().gameObject;
    }

    void OnGUI()
    {
        SelectedObject = Selection.activeGameObject;

        if (SelectedObject == null) return;

        bodySelection.OnGUI();
        headSelection.OnGUI();
        gunSelection.OnGUI();
        //toolSelection.OnGUI(builder);
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
    }
    private void DestroyVehicle()
    {
        DestroyImmediate(GameObject.Find(vehicleName));
    }
    
    private BuilderConfiguration CreateFolderStructure()
    {
        var config = ScriptableObject.CreateInstance<BuilderConfiguration>();

        if (!AssetDatabase.IsValidFolder(config.HomeFolderPath))
            AssetDatabase.CreateFolder("Assets", config.HomeFolderName);

        if (!AssetDatabase.IsValidFolder(config.PrefabsFolderPath))
            AssetDatabase.CreateFolder(config.HomeFolderPath, "Prefabs");

        Type configtype = typeof(BuilderConfiguration);
        IEnumerable<PropertyInfo> props = configtype.GetProperties().
            Where(prop => Attribute.IsDefined(prop, typeof(CustomFolderAttribute)));

        foreach(PropertyInfo propInfo in props)
        {
            var customFolderName = (string)propInfo.GetValue(config);
            var customFolderPath = $"{config.PrefabsFolderPath}\\{customFolderName}";
            if (!AssetDatabase.IsValidFolder(customFolderPath))
                AssetDatabase.CreateFolder(config.PrefabsFolderPath, customFolderName);

            var customFolderAttribute =  (CustomFolderAttribute)propInfo.GetCustomAttribute(typeof(CustomFolderAttribute));
            if (customFolderAttribute.CreateModelsFolderRequired && !AssetDatabase.IsValidFolder(customFolderPath))
                AssetDatabase.CreateFolder(customFolderPath,"Models");
        }
        return config;
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

public class SelectionE
{
    protected int selected = 0;
    protected string[] options;
    public event EventHandler SelectedChanged;
    public int Selected
    {
        get { return selected; }
        private set
        {
            if (selected != value)
            {
                selected = value;
                OnSelectedChanged();
            }
        }
    }
    protected void OnSelectedChanged()
    {
        SelectedChanged?.Invoke(this, new EventArgs());
    }
    public virtual void OnGUI()
    {
        Selected = EditorGUILayout.Popup(Selected, options);
    }

    public SelectionE(IEnumerable<string> options)
    {
        this.options = options.ToArray();
    }
}
public class PartSelectionGUI : SelectionE
{
    string[] paths;
    public PartSelectionGUI(IEnumerable<string> paths) : base(paths)
    {
        this.paths = paths.ToArray();
        options = paths.Select(path => Path.GetFileName(path)).ToArray();
    }
    public string GetSelectedPath()
    {
        return paths[selected];
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

