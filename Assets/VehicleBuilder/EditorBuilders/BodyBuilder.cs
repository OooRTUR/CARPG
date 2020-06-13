using UnityEngine;
using System.Collections;

namespace VehicleBuilder
{
    [ExecuteInEditMode]
    public class BodyBuilder : BaseBuilder
    {
        private BodyBuildData _BodyBuilData { get { return BuildData as BodyBuildData; } }

        protected override void Awake()
        {
            base.Awake();
            Parts.GetVehicleBuilder().PartChanged += BodyBuilder_PartChanged;
        }
        private void Update()
        {
            UpdateHeadPosition();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(HeadPositionHandle, "join.png", false);
        }
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }

        private void UpdateHeadPosition()
        {
            Parts.GetHead().transform.position = HeadPositionHandle;
        }

        private void BodyBuilder_PartChanged(object sender, System.EventArgs e)
        {
            InitBuildData();
        }
        private void InitBuildData()
        {
            BuildData = (BodyBuildData)Context.GetSubAsset(Parts.GetHeadBuilder().Context.GetPartTypeName(), typeof(BodyBuildData));
        }

        public Vector3 HeadPositionHandle
        {
            get { return _BodyBuilData.HeadSurfPosition.GetAbsolutePosition(transform.position); }
            set { _BodyBuilData.HeadSurfPosition.SetRelativePosition(transform.position, value); }
        }

 
    }
}