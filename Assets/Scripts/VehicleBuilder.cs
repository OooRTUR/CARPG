using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleBuilder : MonoBehaviour
{
    [SerializeField]
    private BodyPart bodypart;

    // Start is called before the first frame update
    void Start()
    {
        bodypart = new BodyPart();
        bodypart.Inst(this.transform);
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
