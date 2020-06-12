using UnityEngine;
using UnityEditor;
using System;

namespace VehicleBuilder
{
    public class GunBuildData : BaseBuildData
    {
        public RelativeVector3 JoinPoint { set; get; }
        public RelativeVector3 FirePoint { set; get; }

        private void OnEnable()
        {
            if (JoinPoint == null)
                JoinPoint = new RelativeVector3();
            if (FirePoint == null)
                FirePoint = new RelativeVector3();
        }

    }
}

