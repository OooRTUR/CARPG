using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteInEditMode]
public class VehicleBuilder1 : MonoBehaviour
{
    private Dictionary<string, VehiclePart> parts = new Dictionary<string, VehiclePart>()
    {
        {"Body", new BodyPart() },
        {"Head", new HeadPart() },
        {"Gun", new GunPart() }
    };

    [SerializeField]
    private string bodyPartTypeName = null;
    [SerializeField]
    private string headPartTypeName = null;
    [SerializeField]
    private string gunPartTypeName = null;
    

    // Start is called before the first frame update
    void Start()
    {
        parts["Body"].SpecifyChild("Head", parts["Head"]);
        parts["Head"].SpecifyChild("Gun", parts["Gun"]);


        parts["Gun"].Set(Resources.Load<GameObject>($"Prefabs/Gun/Gun.{gunPartTypeName}"), this.transform);
        parts["Head"].Set(Resources.Load<GameObject>($"Prefabs/Head/Head.{headPartTypeName}"), this.transform);
        parts["Body"].Set(Resources.Load<GameObject>($"Prefabs/Body/Body.{bodyPartTypeName}"), this.transform);        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private string bodyTypeNameEntry;
    private void OnGUI()
    {
        bodyTypeNameEntry = GUILayout.TextField(bodyTypeNameEntry);
        if (GUILayout.Button("Apply Body"))
        {
            GameObject loadedPartPrefab = Resources.Load<GameObject>($"Prefabs/Body/Body.{bodyTypeNameEntry}");
            if (loadedPartPrefab!=null) 
            {
                parts["Body"].Set(loadedPartPrefab, this.transform);
            }
        }
    }
}

[System.Serializable]
public class VehiclePart
{
    public GameObject Obj { get; protected set; }

    protected Dictionary<string, VehiclePart> attachedChilds = new Dictionary<string, VehiclePart>();

    protected Dictionary<string, Vector3> relativePositions = new Dictionary<string, Vector3>();

    public void SpecifyChild(string name, VehiclePart part)
    {
        attachedChilds.Add(name, part);
    }
    public void SpecifyChilds(Dictionary<string, VehiclePart> childs)
    {
        attachedChilds = childs;
    }
    public virtual void Set(GameObject loadedRes, Transform parent)
    {
        if(Obj!=null)
            GameObject.Destroy(Obj);
        Obj = GameObject.Instantiate(loadedRes);
        Obj.transform.parent = parent;
        Obj.transform.position = Vector3.zero;
    }

    public virtual void OnParentPartChanged(string parentTypeName)
    {
        if (relativePositions.ContainsKey(parentTypeName))
        {
            Obj.transform.localPosition = relativePositions[parentTypeName];
        }
        else
            Debug.LogWarning($"There is no such key {parentTypeName}, possible keys: {string.Join(";", relativePositions.Select(x => x.Key))}");
    }
}

[System.Serializable]
public class BodyPart : VehiclePart
{
    public override void Set(GameObject loadedRes,Transform parent)
    {
        base.Set(loadedRes,parent);
        Obj.name = parent.gameObject.name + ".Body";


        string code = loadedRes.name.Substring(loadedRes.name.IndexOf("Body.") + "Body.".Length);
        ((HeadPart)attachedChilds["Head"]).OnParentPartChanged(code);
    }
}

[System.Serializable]
public class HeadPart : VehiclePart
{
    public HeadPart()
    {
        relativePositions = new Dictionary<string, Vector3>()
        {
            {"Muscle",new Vector3(0,0.5f,-1) },
            {"Tank", new Vector3(0,0.5f,1.55f) }
        };
    }

    public override void Set(GameObject loadedRes, Transform parent)
    {
        base.Set(loadedRes, parent);
        Obj.name = parent.gameObject.name + ".Head";

        string loadedResTypeName = loadedRes.name.Substring(loadedRes.name.IndexOf("Head.") + "Head.".Length);
        ((GunPart)attachedChilds["Gun"]).OnParentPartChanged(loadedResTypeName);
    }
}

[System.Serializable]
public class GunPart : VehiclePart
{
    public GunPart()
    {
        relativePositions = new Dictionary<string, Vector3>()
        {
            {"Head", new Vector3(0,0,0.7f) }
        };
    }
    public override void Set(GameObject loadedRes, Transform parent)
    {
        base.Set(loadedRes, parent);
        Obj.name = parent.gameObject.name + ".Gun";

    
    }
}
