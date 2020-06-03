using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GunDirection : MonoBehaviour
{
    public GunDirectionData data;
    private void Start()
    {
        //if(data == null)
        //{
        //    data = new GunDirectionData();
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(data.headPoint, 0.05f);
        Gizmos.DrawSphere(data.firePoint, 0.05f);
    }
}


public class GunDirectionData : ScriptableObject
{
    public Vector3 headPoint;
    public Vector3 firePoint;
}