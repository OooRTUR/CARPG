using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Vehicle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject InstantiatePart(string searchString, string partName)
    {
        GameObject newPart = GameObject.Instantiate<GameObject>((GameObject)Resources.Load(searchString), this.transform);
        newPart.name = partName;
        return newPart;
    }

    public void SetBody(string partTypeName)
    {
        string searchString = $"Prefabs/Body/Body.{partTypeName}";
        Transform existPart = GetBody();
        if (existPart == null)
        {
            InstantiatePart(searchString, "Body");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(searchString, "Body");
        }
    }
    public Transform GetBody()
    {
        return transform.Find("Body");
    }


    public void SetHead(string partTypeName)
    {
        string searchString = $"Prefabs/Head/Head.{partTypeName}";
        Transform existPart = GetHead();
        if (existPart == null)
        {
            InstantiatePart(searchString, "Head");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(searchString, "Head");
        }
    }
    public Transform GetHead()
    {
        return transform.Find("Head");
    }


    public void SetGun(string partTypeName)
    {
        string searchString = $"Prefabs/Gun/Gun.{partTypeName}";
        Transform existPart = GetGun();
        if (existPart == null)
        {
            InstantiatePart(searchString, "Gun");
        }
        else
        {
            DestroyImmediate(existPart.gameObject);
            InstantiatePart(searchString, "Gun");
        }
    }
    public Transform GetGun()
    {
        return transform.Find("Gun");
    }
}
