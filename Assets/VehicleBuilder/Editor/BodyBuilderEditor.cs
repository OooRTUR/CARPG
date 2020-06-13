using UnityEngine;
using UnityEditor;
using System;
using VehicleBuilder;

namespace VehicleBuilder
{    
    [CustomEditor(typeof(BodyBuilder))]
    public class BodyBuilderEditor : Editor
    {
        BodyBuilder builder;
        SelectionE toolSelection;

        private void OnEnable()
        {
            builder = (BodyBuilder)target;
            toolSelection = new SelectionE(new string[]
            {
                "Move Head",
            });
            
        }

        private void OnSceneGUI()
        {
            toolSelection.OnSelected(0, OnToolSelection_MoveHead);
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();
        }

        private void OnToolSelection_MoveHead()
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(BodyBuilder), "HeadPositionHandle");
        }
    }
}