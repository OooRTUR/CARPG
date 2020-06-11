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
                Parts.GetGun().GetComponent<GunBuilder>().UpdatePosition(transform.position, value);
                transform.position = _position;
            }
        }

        public BuilderParts Parts { private set; get; }

        private void OnEnable()
        {
            Parts = new BuilderParts(transform.parent);
        }

    }
}
