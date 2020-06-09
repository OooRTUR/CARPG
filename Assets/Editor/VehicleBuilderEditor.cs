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

    private VehicleBuilder builder;
    private BuilderConfiguration config;
    private GameObject selectedObject;

    private PartSelectionGUI bodySelection;
    private PartSelectionGUI headSelection;
    private PartSelectionGUI gunSelection;

    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set
        {
            if (selectedObject != value)
            {
                selectedObject = value;
            }
        }
    }

    [MenuItem("Tools/Vehicle Builder Window")]
    static void Init()
    {
        VehicleBuilderEditorWindow window = (VehicleBuilderEditorWindow)EditorWindow.GetWindow(typeof(VehicleBuilderEditorWindow));
        window.Show();
    }

    private void OnEnable()
    {
        config = ScriptableObject.CreateInstance<BuilderConfiguration>();

        FileStructureUtile.CreateFolderStructure();
        bodySelection = new PartSelectionGUI(config.GetPartPaths("Body"));
        bodySelection.SelectedChanged += BodySelection_SelectedChanged;

        headSelection = new PartSelectionGUI(config.GetPartPaths("Head"));
        headSelection.SelectedChanged += HeadSelection_SelectedChanged;

        gunSelection = new PartSelectionGUI(config.GetPartPaths("Gun"));
        gunSelection.SelectedChanged += GunSelection_SelectedChanged;

        CreateVehicle();

        Selection.activeObject = builder.gameObject;

    }

    private void OnDestroy()
    {
        DestroyVehicle();
    }

    private void OnGUI()
    {
        SelectedObject = Selection.activeGameObject;

        if (SelectedObject == null) return;

        bodySelection.OnGUI();
        headSelection.OnGUI();
        gunSelection.OnGUI();
    }

    private void BodySelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetBody(config.GetAsset(bodySelection.GetSelectedPath()));
        Selection.activeGameObject = builder.GetBody().gameObject;
    }

    private void HeadSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetHead(config.GetAsset(headSelection.GetSelectedPath()));
        Selection.activeGameObject = builder.GetHead().gameObject;
    }

    private void GunSelection_SelectedChanged(object sender, EventArgs e)
    {
        builder.SetGun(config.GetAsset(gunSelection.GetSelectedPath()));
        Selection.activeGameObject = builder.GetGun().gameObject;
    }

    
    private void CreateVehicle()
    {
        if (GameObject.Find(config.EditName) != null) return;

        GameObject newVehicleObj = new GameObject(config.EditName);
        builder = newVehicleObj.AddComponent<VehicleBuilder>();
        BodySelection_SelectedChanged(null, null);
        HeadSelection_SelectedChanged(null, null);
        GunSelection_SelectedChanged(null, null);
    }
    private void DestroyVehicle()
    {
        DestroyImmediate(GameObject.Find(config.EditName));
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

