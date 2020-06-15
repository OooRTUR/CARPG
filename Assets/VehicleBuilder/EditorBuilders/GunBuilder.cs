using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

namespace VehicleBuilder
{

    [ExecuteInEditMode]
    public class GunBuilder : BaseBuilder
    {
        private GunBuildData _GunBuildData { get { return BuildData as GunBuildData; } }

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(FirePointHandle, "fire.png", false);
            Gizmos.DrawIcon(JoinPointHandle, "join.png", false);
        }

        #region Transform Handles

        public Vector3 JoinPointHandle
        {
            get { return _GunBuildData.JoinPoint.GetAbsolutePosition(transform.position); }
            set
            {
                _GunBuildData.JoinPoint.SetRelativePosition(transform.position, value);
                OnDataChanged();
            }
        }

        public Vector3 FirePointHandle
        {
            get { return _GunBuildData.FirePoint.GetAbsolutePosition(transform.position); }
            set
            {
                _GunBuildData.FirePoint.SetRelativePosition(transform.position, value);
                OnDataChanged();
            }
        }

        public Vector3 PositionHandle
        {
            get { return transform.position; }
            set
            {
                transform.position = value;
            }
        }

        #endregion


    }
}






