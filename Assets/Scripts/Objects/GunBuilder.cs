using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class GunBuilder : MonoBehaviour
{
    public enum Mode { MoveCenter, MoveSurf, SetDirectionPoints }
    public Mode _Mode;
    public GunData gunData;

    public Transform GetSurf()
    {
        return transform.Find("Surf");
    }

    public void OnEnable()
    {
        gunData = (GunData)ScriptableObject.CreateInstance(typeof(GunData));
        gunData.SurfPositionChanged += GunData_SurfPositionChanged;
    }

    private void GunData_SurfPositionChanged(object sender, EventArgs e)
    {
        GetSurf().position = ((GunData)sender).SurfPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (_Mode == Mode.MoveSurf)
        {
            Gizmos.DrawIcon(transform.position, "sphere.png", false);
        }
        Gizmos.DrawIcon(gunData.FirePoint, "fire.png", false);
        Gizmos.DrawIcon(gunData.JoinPoint, "join.png", false);
    }

}

public class GunData : ScriptableObject
{
    public event EventHandler SurfPositionChanged;

    private Vector3 joinPoint;
    private Vector3 firePoint;
    private Vector3 surfPosition;  
    
    public Vector3 JoinPoint
    {
        get { return joinPoint; }
        set { joinPoint = value; }
    }
    public Vector3 FirePoint
    {
        get { return firePoint; }
        set { firePoint = value; }
    }
    public Vector3 SurfPosition
    {
        get { return surfPosition; }
        set 
        { 
            surfPosition = value;
            SurfPositionChanged?.Invoke(this, new EventArgs());
        }
    }



}


