using UnityEngine;
using UnityEditor;
using System;

public class GunBuildData : ScriptableObject
{
    public event EventHandler SurfPositionChanged;

    [SerializeField]
    private Vector3 joinPoint;
    [SerializeField]
    private Vector3 firePoint;
    [SerializeField]
    private Vector3 surfPosition;
    [SerializeField]
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