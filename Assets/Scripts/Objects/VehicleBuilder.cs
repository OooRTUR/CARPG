﻿using UnityEngine;
using System.Collections;

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
    public void SetBody(GameObject partPrefab)
    {
        Transform existPart = GetBody();
        if (existPart == null)
        {
            InstantiatePart(partPrefab, "Body");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(partPrefab, "Body");
        }
    }
    public Transform GetBody()
    {
        return transform.Find("Body");
    }
    #endregion

    #region Head Set/Get
    public void SetHead(GameObject partPrefab)
    {
        Transform existPart = GetHead();
        if (existPart == null)
        {
            InstantiatePart(partPrefab, "Head");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(partPrefab, "Head");
        }
    }
    public Transform GetHead()
    {
        return transform.Find("Head");
    }
    #endregion

    #region Gun Set/Get
    public void SetGun(GameObject partPrefab)
    {
        Transform existPart = GetGun();
        if (existPart == null)
        {
            InstantiatePart(partPrefab, "Gun");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(partPrefab, "Gun");
        }
    }
    public Transform GetGun()
    {
        return transform.Find("Gun");
    }
    #endregion
}