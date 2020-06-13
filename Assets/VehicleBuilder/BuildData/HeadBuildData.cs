using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VehicleBuilder
{
    public class HeadBuildData : BaseBuildData
    {
        [SerializeField]
        private RelativeVector3 gunSurfPosition;
        [SerializeField]
        private RelativeVector3 gunCenterPosition;

        public RelativeVector3 GunSurfPosition
        {
            get { return gunSurfPosition; }
            set { gunSurfPosition = value; }
        }
        public RelativeVector3 GunCenterPosition
        {
            get { return gunCenterPosition; }
            set { gunCenterPosition = value; }
        }

        private void OnEnable()
        {
            if (GunSurfPosition == null)
                GunSurfPosition = new RelativeVector3();
            if (GunCenterPosition == null)
                GunCenterPosition = new RelativeVector3();
        }
    }

}
