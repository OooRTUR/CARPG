using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

public class GUIExtensions
{
    public static void Button(string label, Action action)
    {
        if (GUILayout.Button(label))
        {
            action.Invoke();
        }
    }

    public static void PositionHandle(UnityEngine.Object obj, Type type, string propName)
    {
        PropertyInfo propInfo = type.GetProperty(propName);
        EditorGUI.BeginChangeCheck();
        Vector3 handlePosition = Handles.PositionHandle((Vector3)propInfo.GetValue(obj), Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(obj, "Change Fire Position");
            propInfo.SetValue(obj, handlePosition);
        }
    }

}
