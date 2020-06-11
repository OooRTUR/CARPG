using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

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

    public void SetBody(PartContext partContext)
    {
        Transform existPart = Parts.GetBody();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        InstantiatePart((GameObject)partContext.Prefab, "Body");
    }

    public void SetHead(PartContext partContext)
    {
        Transform existPart = Parts.GetHead();
        if (existPart != null)
        { 
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart((GameObject)partContext.Prefab, "Head");
        res.AddComponent<HeadBuilder>();
    }

    public void SetGun(PartContext partContext)
    {
        Transform existPart = Parts.GetGun();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart((GameObject)partContext.Prefab, "Gun");
        var gunBuilder =  res.AddComponent<GunBuilder>();
        gunBuilder.GunData = (GunBuildData)partContext.Data;
    }
}