using UnityEngine;
using System.Collections;

namespace VehicleBuilder
{
    public class BodyBuildData : BaseBuildData
    {
        [SerializeField]
        private RelativeVector3 headSurfPosition;

        public RelativeVector3 HeadSurfPosition
        {
            get { return headSurfPosition; }
            set { headSurfPosition = value; }
        }

        private void OnEnable()
        {
            if (HeadSurfPosition == null)
                HeadSurfPosition = new RelativeVector3();
        }
    }
}
