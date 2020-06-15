using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using VehicleBuilder;
using Extenstions.Editor;
using Unity.EditorCoroutines.Editor;
using Extensions.Editor;

namespace VehicleBuilder
{    
    [CustomEditor(typeof(BodyBuilder))]
    public class BodyBuilderEditor : BaseBuilderEditor
    {
        SelectionE toolSelection;

        private BodyBuilder Builder { get { return base.builder as BodyBuilder; } }

        protected override void OnEnable()
        {
            base.OnEnable();
            toolSelection = new SelectionE(new string[]
            {
                "Move Head",
            });          
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void OnSceneGUI()
        {
            toolSelection.OnSelected_Invoke(0, OnToolSelection_MoveHead);
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