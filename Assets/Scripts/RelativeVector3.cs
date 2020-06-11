using UnityEngine;

[System.Serializable]
public class RelativeVector3
{
    [SerializeField]
    private Vector3 position = Vector3.zero;
    public void SetRelativePosition(Vector3 source, Vector3 target)
    {
        position = target - source;
    }
    public Vector3 GetAbsolutePosition(Vector3 source)
    {
        return source + position;
    }
}