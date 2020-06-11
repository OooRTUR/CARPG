using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace VehicleBuilder
{

    [CustomEditor(typeof(GunBuilder))]
    public class GunBuilderEditor : Editor
    {
        SelectionE toolSelection;
        SelectionE dirPointSelection;

        GunBuilder builder;

        private void OnEnable()
        {
            builder = (GunBuilder)target;
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

        protected virtual void OnSceneGUI()
        {
            toolSelection.OnSelected(0, OnToolSelection_MoveDirectionPoints);
            toolSelection.OnSelected(1, OnToolSelection_MoveSurf);
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();
            toolSelection.OnSelected(0,dirPointSelection.OnGUI);
            GUIExtensions.Button("Apply Changes", OnApplyButtonPressed);
        }

        private void OnToolSelection_MoveSurf()
        {
            builder._Mode = GunBuilder.Mode.MoveSurf;
            Tools.current = Tool.Custom;

            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "PositionHandle");
        }

        private void OnToolSelection_MoveDirectionPoints()
        {
            builder._Mode = GunBuilder.Mode.SetDirectionPoints;
            Tools.current = Tool.Custom;

            dirPointSelection.OnSelected(0, FirePointPositionHandle);
            dirPointSelection.OnSelected(1, JoinPointPositionHandle);
        }

        private void FirePointPositionHandle() 
        {
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "FirePointHandle");
        }
        private void JoinPointPositionHandle()
        {
            GUIExtensions.PositionHandle(builder, typeof(GunBuilder), "JoinPointHandle");
        }

        private void OnApplyButtonPressed()
        {
            AssetDatabase.Refresh();
        }
    }

}
