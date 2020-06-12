using UnityEngine;
using UnityEditor;
using System;

namespace VehicleBuilder
{
    public class GunBuildData : BaseBuildData
    {
        [SerializeField]
        private RelativeVector3 joinPoint;
        [SerializeField]
        private RelativeVector3 firePoint;
        public RelativeVector3 JoinPoint
        {
            set { joinPoint = value; }
            get { return joinPoint; }
        }
        public RelativeVector3 FirePoint
        {
            get { return firePoint; }
            set { firePoint = value; }
        }

        private void OnEnable()
        {
            if (JoinPoint == null)
                JoinPoint = new RelativeVector3();
            if (FirePoint == null)
                FirePoint = new RelativeVector3();
        }

    }
}

