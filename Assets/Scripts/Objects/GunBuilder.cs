using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GunBuilder : MonoBehaviour
{
    public Transform GetSurf()
    {
        return transform.Find("Surf");
    }

    private Vector3 _newSurfPosition = Vector3.zero;
    public Vector3 NewSurfPosition
    {
        get { return _newSurfPosition; }
        set { _newSurfPosition = value; }
    }

    public virtual void ApplySurfPosition()
    {
        GetSurf().position = _newSurfPosition;
    }

    public Vector3 JoinPosition { get; set; }
    public Vector3 FirePosition { get; set; }

    private void OnDrawGizmosSelected()
    {
        if (_Mode == Mode.MoveSurf)
        {
            Gizmos.DrawIcon(transform.position, "sphere.png", false);
        }
        Gizmos.DrawIcon(FirePosition, "fire.png", false);
        Gizmos.DrawIcon(JoinPosition, "join.png", false);
    }
    public enum Mode { MoveCenter, MoveSurf, SetDirectionPoints}
    public Mode _Mode;
}


