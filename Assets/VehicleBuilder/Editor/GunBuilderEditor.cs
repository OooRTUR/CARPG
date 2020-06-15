using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;
using Extenstions.Editor;
using Extensions.Editor;

namespace VehicleBuilder
{

    [CustomEditor(typeof(GunBuilder))]
    public class GunBuilderEditor : BaseBuilderEditor
    {
        SelectionE toolSelection;
        SelectionE dirPointSelection;

        private GunBuilder Builder { get { return base.builder as GunBuilder; } }

        protected override void OnEnable()
        {
            base.OnEnable();
            toolSelection = new SelectionE(new string[]{
                "Move Direction Points",
                "Move Surf",
                "Move Center",
            });
            dirPointSelection = new SelectionE(new string[]
            {
                "Fire Point",
                "Join Point"
            });

 
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected virtual void OnSceneGUI()
        {
            toolSelection.OnSelected_Invoke(0, OnToolSelection_MoveDirectionPoints);
            toolSelection.OnSelected_Invoke(1, OnToolSelection_MoveSurf);
            EditorUtility.SetDirty((builder as GunBuilder).BuildData);
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();
            toolSelection.OnSelected_Invoke(0,dirPointSelection.OnGUI);
        }

        private void OnToolSelection_MoveSurf()
        {
            Builder._Mode = GunBuilder.Mode.MoveSurf;
            Tools.current = Tool.Custom;

            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "PositionHandle");
        }

        private void OnToolSelection_MoveDirectionPoints()
        {
            Builder._Mode = GunBuilder.Mode.SetDirectionPoints;
            Tools.current = Tool.Custom;

            dirPointSelection.OnSelected_Invoke(0, FirePointPositionHandle);
            dirPointSelection.OnSelected_Invoke(1, JoinPointPositionHandle);
        }

        private void FirePointPositionHandle() 
        {
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "FirePointHandle");
        }
        private void JoinPointPositionHandle()
        {
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "JoinPointHandle");
        }
    }

}
