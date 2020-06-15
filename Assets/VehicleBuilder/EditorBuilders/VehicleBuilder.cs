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
        public event EventHandler PartChanged;
        private Dictionary<string, VehiclePartContext> contextStorage;
        Queue<Action> tasks;
        private BuilderParts parts;

        private void Awake()
        {
            tasks = new Queue<Action>();
            parts = new BuilderParts(transform);
            contextStorage = new Dictionary<string, VehiclePartContext>()
            {
                {nameof(VehicleBuilder), null },
                {nameof(HeadBuilder), null },
                {nameof(BodyBuilder), null }
            };
        }


        private void Update()
        {
            while (tasks.Count > 0)
            {
                Action currentAction = tasks.Dequeue();
                currentAction.Invoke();
                if (tasks.Count == 0)
                    PartChanged?.Invoke(this, new EventArgs());
            }
        }

        public VehiclePartContext GetContext(string partHierarchyTypeName)
        {
            return contextStorage[partHierarchyTypeName];
        }

        private GameObject InstantiatePart(GameObject partPrefab, string partName)
        {
            GameObject newPart = GameObject.Instantiate<GameObject>(partPrefab, this.transform);
            newPart.name = partName;
            return newPart;
        }

        #region Set Part Methods
        public void SetBody(VehiclePartContext partContext)
        {
            tasks.Enqueue(delegate
            {
                contextStorage[nameof(BodyBuilder)] = partContext;
                Transform existPart = parts.GetBody();
                if (existPart != null)
                {
                    DestroyImmediate(existPart.gameObject);
                }
                var res = InstantiatePart((GameObject)partContext.GetPrefabDirectly(), "Body");
                res.AddComponent<BodyBuilder>();
            });

        }

        public void SetHead(VehiclePartContext partContext)
        {
            tasks.Enqueue(delegate
            {
                contextStorage[nameof(HeadBuilder)] = partContext;
                Transform existPart = parts.GetHead();
                if (existPart != null)
                {
                    DestroyImmediate(existPart.gameObject);
                }
                GameObject res = InstantiatePart((GameObject)partContext.GetPrefabDirectly(), "Head");
                var headBuilder = res.AddComponent<HeadBuilder>();
            });

        }

        public void SetGun(VehiclePartContext partContext)
        {
            tasks.Enqueue(delegate
            {
                contextStorage[nameof(GunBuilder)] = partContext;
                Transform existPart = parts.GetGun();
                if (existPart != null)
                {
                    DestroyImmediate(existPart.gameObject);
                }
                GameObject res = InstantiatePart((GameObject)partContext.GetPrefabDirectly(), "Gun");
                var gunBuilder = res.AddComponent<GunBuilder>();
            });

        }

        #endregion
    }
}