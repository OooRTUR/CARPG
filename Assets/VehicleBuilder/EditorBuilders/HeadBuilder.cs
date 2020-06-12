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
        }


        private void Start()
        {
//            string path = AssetDatabase.GetAssetPath(HeadData);
//            UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
//            string gunTypeName = Parts.GetGun().GetComponent<GunBuilder>().GunData.PartTypeName;
//            List<UnityEngine.Object> res = assets.
//                Where(asset => ((HeadBuildData)asset).AttachedGunTypeName == gunTypeName).ToList();
//            if (res.Count == 0)
//            {
//                HeadData = (HeadBuildData)ScriptableObject.CreateInstance(typeof(HeadBuildData));
//                HeadData.PartTypeName = Path.GetFileNameWithoutExtension(path);
//                HeadData.AttachedGunTypeName = gunTypeName;
//                AssetDatabase.AddObjectToAsset(HeadData, path);
//            }
//            else
//            {
//z                HeadData = (HeadBuildData)res[0];
//            }
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
