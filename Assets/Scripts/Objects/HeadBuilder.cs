using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public class HeadBuilder : MonoBehaviour
{
    private Vector3 _position = Vector3.zero;
    public Vector3 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public virtual void Change()
    {
        transform.position = _position;
        GetGun().position = _position;
    }

    public Head HeadComponent()
    {
        return transform.GetComponent<Head>();
    }
    public Transform GetGun()
    {
        return transform.parent.Find("Gun");
    }
}
