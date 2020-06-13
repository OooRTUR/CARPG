using UnityEngine;
using UnityEditor;

public class BaseBuildData : ScriptableObject
{
    public override bool Equals(object other)
    {
        if (other != null)
        {
            return base.name == ((UnityEngine.Object)other).name;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.name.GetHashCode();
    }
}