using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace VehicleBuilder
{
    public class BuilderParts
    {
        private Transform transform;
        public BuilderParts(Transform transform)
        {
            this.transform = transform;
        }
        public Transform GetBody()
        {
            return transform.Find("Body");
        }

        public Transform GetGun()
        {
            return transform.Find("Gun");
        }

        public Transform GetHead()
        {
            return transform.Find("Head");
        }

        public GunBuilder GetGunBuilder()
        {
            return GetGun().GetComponent<GunBuilder>();
        }
        public HeadBuilder GetHeadBuilder()
        {
            return GetHead().GetComponent<HeadBuilder>();
        }

        public VehicleBuilder GetVehicleBuilder()
        {
            return transform.GetComponent<VehicleBuilder>();
        }
    }
}