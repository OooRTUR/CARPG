﻿using UnityEngine;
using UnityEditor;

namespace VehicleBuilder
{
    [CustomEditor(typeof(HeadBuilder))]
    public class HeadBuilderEditor : Editor
    {
        HeadBuilder builder;
        SelectionE toolSelection;

        protected virtual void OnSceneGUI()
        {

            toolSelection.OnSelected(0, OnToolSelection_MoveHead);
            toolSelection.OnSelected(1, OnToolSelection_MoveGunCenter);
            toolSelection.OnSelected(2, OnToolSelection_MoveGunSurf);

            
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();
            GUIExtensions.Button("Apply Changes", ApplyChanges);
        }


        #region gui actions
        private void ApplyChanges()
        {
            AssetDatabase.Refresh();
        }
        private void OnToolSelection_MoveHead()
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(HeadBuilder), "PositionHandle");
        }
        private void OnToolSelection_MoveGunCenter()
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(HeadBuilder), "GunCenterPositionHandle");
        }
        private void OnToolSelection_MoveGunSurf()
        {
            Tools.current = Tool.Custom;
            GUIExtensions.PositionHandle(builder, typeof(HeadBuilder), "GunSurfPositionHandle");
        }
        #endregion


        private void OnEnable()
        {
            builder = (HeadBuilder)target;
            toolSelection = new SelectionE(new string[]
            {
                "Move Head",
                "Move Gun Center",
                "Move Gun Surf"
            });
        }


    }
}