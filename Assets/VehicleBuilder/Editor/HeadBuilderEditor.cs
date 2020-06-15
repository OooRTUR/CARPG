using UnityEngine;
using UnityEditor;
using Extenstions.Editor;
using Extensions.Editor;

namespace VehicleBuilder
{

    [CustomEditor(typeof(HeadBuilder))]
    public class HeadBuilderEditor : BaseBuilderEditor
    {
        SelectionE toolSelection;

        private HeadBuilder Builder { get { return base.builder as HeadBuilder; } }

        protected override void OnEnable()
        {
            base.OnEnable();
            toolSelection = new SelectionE(new string[]
            {
                "Move Head",
                "Move Gun Center",
                "Move Gun Surf"
            });
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected virtual void OnSceneGUI()
        {

            toolSelection.OnSelected_Invoke(0, OnToolSelection_MoveHead);
            toolSelection.OnSelected_Invoke(1, OnToolSelection_MoveGunCenter);
            toolSelection.OnSelected_Invoke(2, OnToolSelection_MoveGunSurf);

            
        }

        public override void OnInspectorGUI()
        {
            toolSelection.OnGUI();
        }


        #region gui actions
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


 


    }
}