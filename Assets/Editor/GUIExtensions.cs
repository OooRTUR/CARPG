using UnityEngine;
using System;

public class GUIExtensions
{
    public static void Button(string label, Action action)
    {
        if (GUILayout.Button(label))
        {
            action.Invoke();
        }
    }
}
