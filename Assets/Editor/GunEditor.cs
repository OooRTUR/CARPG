using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GunEditor : ScriptableObject
{
    [MenuItem("Tools/MyTool/Create Gun Object")]
    static void DoIt()
    {
        if (Selection.activeObject == null)
            return;
    }
}