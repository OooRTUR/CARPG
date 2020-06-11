using UnityEngine;
using UnityEditor;

namespace VehicleBuilder
{
    [CustomEditor(typeof(HeadBuilder))]
    public class HeadBuilderEditor : Editor
    {
        protected virtual void OnSceneGUI()
        {
            HeadBuilder headBuilder = (HeadBuilder)target;
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(headBuilder, typeof(HeadBuilder), "Position");
        }
    }
}