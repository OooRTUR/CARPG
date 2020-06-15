using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace VehicleBuilder
{
    public class BaseBuilder : MonoBehaviour
    {
        public event EventHandler DataChanged;
        public VehiclePartContext Context { set; get; }
        public BuilderParts Parts { get; private set; }
        public BaseBuildData BuildData { get; protected set; }        
        protected virtual void Awake()
        {
            Parts = new BuilderParts(transform.parent);
            Context = Parts.GetVehicleBuilder().GetContext(this.GetType().Name);
            BuildData = (BaseBuildData)Context.GetAssetDirectly();
            Parts.GetVehicleBuilder().PartChanged += delegate
            {
                InitBaseBuildData();
            };
        }
        protected void OnDataChanged()
        {
            DataChanged?.Invoke(this, new EventArgs());
        }

        protected virtual void InitBaseBuildData()
        {

        }

        void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
                UnityEditor.SceneView.RepaintAll();
            }
#endif
        }
    }
}