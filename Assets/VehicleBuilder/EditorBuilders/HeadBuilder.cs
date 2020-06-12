using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using System.Linq;
using System.IO;

namespace VehicleBuilder
{
    [ExecuteInEditMode]
    public class HeadBuilder : BaseBuilder
    {
        private Vector3 _position = Vector3.zero;

        private HeadBuildData _HeadBuilData { get { return BuildData as HeadBuildData; } }

        protected override void Awake()
        {
            base.Awake();
            Parts.GetVehicleBuilder().GunChanged += HeadBuilder_GunChanged;
            
        }

        private void HeadBuilder_GunChanged(object sender, EventArgs e)
        {
            InitBuildData();
        }

        private void InitBuildData()
        {
            BuildData = (HeadBuildData)Context.GetSubAsset(Parts.GetGunBuilder().Context.GetPartTypeName(), typeof(HeadBuildData));
        }


        private void Start()
        {

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(GunSurfPositionHandle, "fire.png", false);
            Gizmos.DrawIcon(GunCenterPositionHandle, "join.png", false);
        }

        public Vector3 PositionHandle
        {
            get { return _position; }
            set
            {

                transform.position = _position;
                Parts.GetGun().GetComponent<GunBuilder>().PositionHandle = transform.position;
            }
        }

        public Vector3 GunSurfPositionHandle
        {
            get { return _HeadBuilData.GunSurfPosition.GetAbsolutePosition(transform.position); }
            set { _HeadBuilData.GunSurfPosition.SetRelativePosition(transform.position, value); }
        }

        public Vector3 GunCenterPositionHandle
        {
            get { return _HeadBuilData.GunCenterPosition.GetAbsolutePosition(transform.position); }
            set { _HeadBuilData.GunCenterPosition.SetRelativePosition(transform.position, value); }
        }

    }
}
