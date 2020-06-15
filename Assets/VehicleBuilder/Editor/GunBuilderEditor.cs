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
                "Fire Point",
                "Join Point"
            });
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();

        }

        protected virtual void OnSceneGUI()
        {
            dirPointSelection.OnSelected_Invoke(0, FirePointPositionHandle);
            dirPointSelection.OnSelected_Invoke(1, JoinPointPositionHandle);
            EditorUtility.SetDirty(Builder.BuildData);
        }

        private void FirePointPositionHandle() 
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "FirePointHandle");
        }
        private void JoinPointPositionHandle()
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "JoinPointHandle");
        }
    }

}
