using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{
    private GameObject body;
    private GameObject head;
    private GameObject gun;

    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {

        //SetBody();
        //SetHead();
        //SetGun();
        
    }

    //private void SetBody()
    //{
    //    body = Instantiate(RuntimeAssets.Instance.bodyParts[0], transform);
    //}
    //private void SetHead()
    //{
    //    head = Instantiate(RuntimeAssets.Instance.headParts[0], transform);
    //}
    //private void SetGun()
    //{
    //    gun = Instantiate(RuntimeAssets.Instance.gunParts[0], transform);
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
