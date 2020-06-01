using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VehicleBuilder : MonoBehaviour
{
    private Dictionary<string, VehiclePart> parts = new Dictionary<string, VehiclePart>()
    {
        {"Body", new BodyPart() },
        {"Head", new HeadPart() }
    };

    

    // Start is called before the first frame update
    void Start()
    {
        parts["Body"].SpecifyChild("Head", parts["Head"]);

        parts["Head"].Set(Resources.Load<GameObject>("Prefabs/Head/Head"), this.transform);
        parts["Body"].Set(Resources.Load<GameObject>("Prefabs/Body/Body.Muscle"), this.transform);        
        
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
    private static Dictionary<string, Vector3> relativePositions = new Dictionary<string, Vector3>()
    {
        {"Muscle",new Vector3(0,0.5f,-1) },
        {"Tank", new Vector3(0,0.5f,1.55f) }
    };
    public static Dictionary<string, Vector3> RelativePositions
    {
        get { return new Dictionary<string, Vector3>(relativePositions); }
    }

    private GunPart attachedGun;

    public override void Set(GameObject loadedRes, Transform parent)
    {
        base.Set(loadedRes, parent);
        Obj.name = parent.gameObject.name + ".Head";
    }

    public void OnParentPartChanged(string parentTypeName)
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
public class GunPart : VehiclePart
{
    public override void Set(GameObject loadedRes, Transform parent)
    {
        base.Set(loadedRes, parent);
    }
}
