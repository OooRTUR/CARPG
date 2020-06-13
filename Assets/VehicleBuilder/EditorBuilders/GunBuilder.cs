using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

namespace VehicleBuilder
{

    [ExecuteInEditMode]
    public class GunBuilder : BaseBuilder
    {
        public enum Mode { MoveCenter, MoveSurf, SetDirectionPoints }
        public Mode _Mode;
        
        private GunBuildData _GunBuildData { get { return BuildData as GunBuildData; } }

        protected override void Awake()
        {
            base.Awake();
        }

        #region Vector3 Handles
        public Vector3 JoinPointHandle
        {
            get { return _GunBuildData.JoinPoint.GetAbsolutePosition(transform.position); }
            set { _GunBuildData.JoinPoint.SetRelativePosition(transform.position, value); }
        }

        public Vector3 FirePointHandle
        {
            get { return _GunBuildData.FirePoint.GetAbsolutePosition(transform.position); }
            set { _GunBuildData.FirePoint.SetRelativePosition(transform.position, value); }
        }

        public Vector3 PositionHandle
        {
            get { return transform.position; }
            set
            {
                transform.position = value;
                OnPositionChanged();
            }
        }
        #endregion

        public void UpdatePosition(Vector3 oldPosition, Vector3 newPosition)
        {

        }



        private void OnDrawGizmosSelected()
        {
            if (_Mode == Mode.MoveSurf)
            {
                Gizmos.DrawIcon(transform.position, "sphere.png", false);
            }
            Gizmos.DrawIcon(FirePointHandle, "fire.png", false);
            Gizmos.DrawIcon(JoinPointHandle, "join.png", false);
        }

   


        private void OnPositionChanged()
        {

        }


    }
}






