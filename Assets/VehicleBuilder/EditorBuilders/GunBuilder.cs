using UnityEngine;
using System.Collections;
using System;

namespace VehicleBuilder
{

    [ExecuteInEditMode]
    public class GunBuilder : MonoBehaviour
    {
        public enum Mode { MoveCenter, MoveSurf, SetDirectionPoints }
        public Mode _Mode;
        private GunBuildData gunData;
        public GunBuildData GunData
        {
            get { return gunData; }
            set
            {
                gunData = value;
            }
        }
        public BuilderParts Parts { get; private set; }

        #region Vector3 Handles
        public Vector3 JoinPointHandle
        {
            get { return gunData.JoinPoint.GetAbsolutePosition(transform.position); }
            set { gunData.JoinPoint.SetRelativePosition(transform.position, value); }
        }

        public Vector3 FirePointHandle
        {
            get { return gunData.FirePoint.GetAbsolutePosition(transform.position); }
            set { gunData.FirePoint.SetRelativePosition(transform.position, value); }
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


        public void OnEnable()
        {
            Parts = new BuilderParts(transform.parent);
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






