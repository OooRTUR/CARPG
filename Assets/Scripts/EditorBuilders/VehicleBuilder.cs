using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class VehicleBuilder : MonoBehaviour
{
    public BuilderParts Parts { get; private set; }

    private void Awake()
    {
        Parts = new BuilderParts(transform);
    }

    private GameObject InstantiatePart(GameObject partPrefab, string partName)
    {
        GameObject newPart = GameObject.Instantiate<GameObject>(partPrefab, this.transform);
        newPart.name = partName;
        return newPart;
    }

    public void SetBody(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = Parts.GetBody();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        InstantiatePart(partData.Item1, "Body");
    }

    public void SetHead(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = Parts.GetHead();
        if (existPart != null)
        { 
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart(partData.Item1, "Head");
        res.AddComponent<HeadBuilder>();
    }

    public void SetGun(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = Parts.GetGun();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart(partData.Item1, "Gun");
        res.AddComponent<GunBuilder>();

    }
}