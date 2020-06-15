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
        }

        protected override void InitBaseBuildData()
        {
            string partTypeName = Parts.GetHeadBuilder().Context.GetPartTypeName();
            BuildData = (BodyBuildData)Context.GetSubAssetDirectly(partTypeName, typeof(BodyBuildData));
        }

        private void Update()
        {
            UpdateHeadPosition();
        }

        private void UpdateHeadPosition()
        {
            Parts.GetHead().transform.position = HeadPositionHandle;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(HeadPositionHandle, "join.png", false);
        }

        #region transform Handles
        public Vector3 HeadPositionHandle
        {
            get { return _BodyBuilData.HeadSurfPosition.GetAbsolutePosition(transform.position); }
            set 
            { 
                _BodyBuilData.HeadSurfPosition.SetRelativePosition(transform.position, value);
                OnDataChanged();
            }
        }

        #endregion


    }
}