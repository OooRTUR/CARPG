using UnityEngine;
using System.Collections;

namespace VehicleBuilder
{
    public class BaseBuilder : MonoBehaviour
    {
        public VehiclePartContext Context { set; get; }
        public BuilderParts Parts { get; private set; }
        public BaseBuildData BuildData { get; protected set; }        
        protected virtual void Awake()
        {
            Parts = new BuilderParts(transform.parent);
            Context = Parts.GetVehicleBuilder().GetContext(this.GetType().Name);
            BuildData = (BaseBuildData)Context.GetAsset();
        }
    }
}