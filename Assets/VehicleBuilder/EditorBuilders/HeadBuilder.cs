using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace VehicleBuilder
{
    [ExecuteInEditMode]
    public class HeadBuilder : MonoBehaviour
    {
        private Vector3 _position = Vector3.zero;
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                
                transform.position = _position;
                Parts.GetGun().GetComponent<GunBuilder>().PositionHandle = transform.position;
            }
        }

        public HeadBuildData HeadData { get; set; }

        public BuilderParts Parts { private set; get; }

        private void OnEnable()
        {
            Parts = new BuilderParts(transform.parent);
        }

        public Vector3 GunSurfPositionHandle
        {
            get { return HeadData.GunSurfPosition.GetAbsolutePosition(transform.position); }
            set { HeadData.GunSurfPosition.SetRelativePosition(transform.position, value); }
        }

        public Vector3 GunCenterPositionHandle
        {
            get { return HeadData.GunCenterPosition.GetAbsolutePosition(transform.position); }
            set { HeadData.GunCenterPosition.SetRelativePosition(transform.position, value); }
        }

    }
}
