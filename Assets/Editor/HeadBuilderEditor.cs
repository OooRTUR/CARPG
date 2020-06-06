using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HeadBuilder))]
public class HeadBuilderEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        HeadBuilder headBuilder = (HeadBuilder)target;
        Tools.current = Tool.Custom;

        EditorGUI.BeginChangeCheck();
        Vector3 handlePosition = Handles.PositionHandle(headBuilder.transform.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(headBuilder, "Change Look At Target Position");
            headBuilder.Position = handlePosition;
            headBuilder.ApplyPosition();
        }
    }
}