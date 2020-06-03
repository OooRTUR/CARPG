using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GunDirection))]
public class GunDirectionEditor : Editor
{
    private void OnEnable()
    {
        
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        serializedObject.ApplyModifiedProperties();
    }
}