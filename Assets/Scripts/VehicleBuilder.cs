using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBuilder : MonoBehaviour
{
    [SerializeField]
    private BodyPart bodypart;
    [SerializeField]
    private HeadPart headpart;

    // Start is called before the first frame update
    void Start()
    {
        bodypart.Inst(this.transform);
        headpart.Inst(this.transform);
        headpart.OnParentPartChanged("Tank");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class VehiclePart
{
    [SerializeField]
    private GameObject prefab;

    public GameObject Obj { get; private set; }

    public virtual void Inst(Transform parent)
    {
        Obj = GameObject.Instantiate(prefab);
        Obj.transform.parent = parent;
    }
}

[System.Serializable]
public class BodyPart : VehiclePart
{
    public override void Inst(Transform parent)
    {
        base.Inst(parent);
        Obj.name = parent.gameObject.name + ".Body";
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

    public override void Inst(Transform parent)
    {
        base.Inst(parent);
        Obj.name = parent.gameObject.name + ".Head";
    }

    public void OnParentPartChanged(string parentTypeName)
    {
        Obj.transform.localPosition = relativePositions[parentTypeName];
    }
}
