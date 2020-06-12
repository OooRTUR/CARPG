using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VehicleBuilder
{
    public class HeadBuildData : BaseBuildData
    {
        public string AttachedGunTypeName { set; get; }

        public RelativeVector3 GunSurfPosition { set; get; }
        public RelativeVector3 GunCenterPosition { set; get; }

        private void OnEnable()
        {
            if (GunSurfPosition == null)
                GunSurfPosition = new RelativeVector3();
            if (GunCenterPosition == null)
                GunCenterPosition = new RelativeVector3();
        }
    }

}
