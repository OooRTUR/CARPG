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
        Vector3 newTargetPosition = Handles.PositionHandle(headBuilder.Position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(headBuilder, "Change Look At Target Position");
            headBuilder.Position = newTargetPosition;
            headBuilder.Change();
        }
    }
}