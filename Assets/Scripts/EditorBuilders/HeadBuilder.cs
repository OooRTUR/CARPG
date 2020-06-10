using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
public class HeadBuilder : MonoBehaviour
{
    private Vector3 _position = Vector3.zero;
    public Vector3 Position
    {
        get { return _position; }
        set 
        {
            Vector3 substract = value - _position;
            _position += substract;
            Parts.GetGun().GetComponent<GunBuilder>().UpdatePosition(substract);
            transform.position = _position;
        }
    }

    public BuilderParts Parts { private set; get; }

    private void OnEnable()
    {
        Parts = new BuilderParts(transform.parent);
    }

}
