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
                gunData.SurfPositionChanged += UpdateSurfPosition;
                UpdateSurfPosition(GunData, null);
            }
        }
        public BuilderParts Parts { get; private set; }

        public string prefabGuid;

        public void UpdatePosition(Vector3 subtract)
        {
            gunData.CenterPosition += subtract;
            gunData.SurfPosition += subtract;
            gunData.JoinPoint += subtract;
            gunData.FirePoint += subtract;
        }


        public void OnEnable()
        {
            Parts = new BuilderParts(transform.parent);

            if (GunData == null)
                GunData = (GunBuildData)ScriptableObject.CreateInstance(typeof(GunBuildData));
        }

        private void OnDrawGizmosSelected()
        {
            if (_Mode == Mode.MoveSurf)
            {
                Gizmos.DrawIcon(transform.position, "sphere.png", false);
            }
            Gizmos.DrawIcon(gunData.FirePoint, "fire.png", false);
            Gizmos.DrawIcon(gunData.JoinPoint, "join.png", false);
            Gizmos.DrawIcon(gunData.CenterPosition, "center.png", false);
        }

        private void UpdateSurfPosition(object sender, EventArgs e)
        {
            transform.position = ((GunBuildData)sender).SurfPosition;
        }


    }
}






