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

            toolSelection.OnSelected(0, OnToolSelection_MoveHead);
            toolSelection.OnSelected(1, OnToolSelection_MoveGunCenter);
            toolSelection.OnSelected(2, OnToolSelection_MoveGunSurf);
        }

        private void OnToolSelection_MoveHead()
        {

        }
        private void OnToolSelection_MoveGunCenter()
        {

        }
        private void OnToolSelection_MoveGunSurf()
        {

        }

        SelectionE toolSelection;

        private void OnEnable()
        {
            toolSelection = new SelectionE(new string[]
            {
                "Move Head",
                "Move Gun Center",
                "Move Gun Surf"
            });
        }

        public override void OnInspectorGUI()
        {
            
        }
    }
}