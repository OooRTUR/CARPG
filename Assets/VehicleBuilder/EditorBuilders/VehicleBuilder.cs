using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;

namespace VehicleBuilder
{
    [ExecuteInEditMode]
    public class VehicleBuilder : MonoBehaviour
    {
        public BuilderParts Parts { get; private set; }
        private Dictionary<string, VehiclePartContext> contextStorage;

        private void Awake()
        {
            Parts = new BuilderParts(transform);
            contextStorage = new Dictionary<string, VehiclePartContext>()
            {
                {nameof(VehicleBuilder), null },
                {nameof(HeadBuilder), null },
            };
        }

        private GameObject InstantiatePart(GameObject partPrefab, string partName)
        {
            GameObject newPart = GameObject.Instantiate<GameObject>(partPrefab, this.transform);
            newPart.name = partName;
            return newPart;
        }


        public VehiclePartContext GetContext(string partHierarchyTypeName)
        {
            return contextStorage[partHierarchyTypeName];
        }

        public void SetBody(VehiclePartContext partContext)
        {
            Transform existPart = Parts.GetBody();
            if (existPart != null)
            {
                DestroyImmediate(existPart.gameObject);
            }
            InstantiatePart((GameObject)partContext.GetPrefab(), "Body");
        }

        public void SetHead(VehiclePartContext partContext)
        {
            contextStorage[nameof(HeadBuilder)] = partContext;
            Transform existPart = Parts.GetHead();
            if (existPart != null)
            {
                DestroyImmediate(existPart.gameObject);
            }
            GameObject res = InstantiatePart((GameObject)partContext.GetPrefab(), "Head");
            var headBuilder = res.AddComponent<HeadBuilder>();
        }
        public event EventHandler GunChanged;

        public void SetGun(VehiclePartContext partContext)
        {
            contextStorage[nameof(GunBuilder)] = partContext;
            Transform existPart = Parts.GetGun();
            if (existPart != null)
            {
                DestroyImmediate(existPart.gameObject);
            }
            GameObject res = InstantiatePart((GameObject)partContext.GetPrefab(), "Gun");
            var gunBuilder = res.AddComponent<GunBuilder>();
            GunChanged?.Invoke(this, new EventArgs());
        }
    }
}