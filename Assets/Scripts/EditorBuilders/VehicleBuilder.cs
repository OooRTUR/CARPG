using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class VehicleBuilder : MonoBehaviour
{

    private GameObject InstantiatePart(GameObject partPrefab, string partName)
    {
        GameObject newPart = GameObject.Instantiate<GameObject>(partPrefab, this.transform);
        newPart.name = partName;
        return newPart;
    }

    #region Body Set/Get
    public void SetBody(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = GetBody();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        InstantiatePart(partData.Item1, "Body");
    }
    public Transform GetBody()
    {
        return transform.Find("Body");
    }
    #endregion

    #region Head Set/Get
    public void SetHead(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = GetHead();
        if (existPart != null)
        { 
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart(partData.Item1, "Head");
        res.AddComponent<HeadBuilder>();
    }
    public Transform GetHead()
    {
        return transform.Find("Head");
    }
    #endregion

    #region Gun Set/Get
    public void SetGun(Tuple<GameObject, UnityEngine.Object> partData)
    {
        Transform existPart = GetGun();
        if (existPart != null)
        {
            DestroyImmediate(existPart.gameObject);
        }
        GameObject res = InstantiatePart(partData.Item1, "Gun");
        res.AddComponent<GunBuilder>();

    }
    public Transform GetGun()
    {
        return transform.Find("Gun");
    }
    #endregion
}
