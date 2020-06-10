using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class GunBuilder : MonoBehaviour
{
    public enum Mode { MoveCenter, MoveSurf, SetDirectionPoints }
    public Mode _Mode;
    public GunData gunData;
    public BuilderParts Parts { get; private set;}
    public void UpdatePosition(Vector3 subtract)
    {
        gunData.CenterPosition += subtract;
        gunData.SurfPosition += subtract;
        gunData.JoinPoint += subtract;
        gunData.FirePoint += subtract;
    }


    public void OnEnable()
    {
        Parts = new BuilderParts(transform.parent);

        gunData = (GunData)ScriptableObject.CreateInstance(typeof(GunData));
        gunData.SurfPositionChanged += GunData_SurfPositionChanged;
    }


    private void OnDrawGizmosSelected()
    {
        if (_Mode == Mode.MoveSurf)
        {
            Gizmos.DrawIcon(transform.position, "sphere.png", false);
        }
        Gizmos.DrawIcon(gunData.FirePoint, "fire.png", false);
        Gizmos.DrawIcon(gunData.JoinPoint, "join.png", false);
        Gizmos.DrawIcon(gunData.CenterPosition, "center.png", false);
    }

    private void GunData_SurfPositionChanged(object sender, EventArgs e)
    {
        transform.position = ((GunData)sender).SurfPosition;
    }
}

public class BuilderParts
{
    private Transform transform;
    public BuilderParts(Transform transform)
    {
        this.transform = transform;
    }
    public Transform GetBody()
    {
        return transform.Find("Body");
    }

    public Transform GetGun()
    {
        return transform.Find("Gun");
    }

    public Transform GetHead()
    {
        return transform.Find("Head");
    }
}

public class GunData : ScriptableObject
{
    public event EventHandler SurfPositionChanged;

    private Vector3 joinPoint;
    private Vector3 firePoint;
    private Vector3 surfPosition;
    private Vector3 centerPosition;
    
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
    public Vector3 CenterPosition
    {
        get { return centerPosition; }
        set { centerPosition = value; }
    }



}


