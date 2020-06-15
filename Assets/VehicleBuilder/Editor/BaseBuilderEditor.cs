using Extensions.Editor;
using UnityEditor;

namespace VehicleBuilder
{
    public class BaseBuilderEditor : Editor
    {
        protected BaseBuilder builder;
        protected CoroutineTimer refreshTimer;

        protected virtual void OnEnable()
        {
            builder = (BaseBuilder)target;
            refreshTimer = new CoroutineTimer(8.0f);
            refreshTimer.StartCoroutine();
            refreshTimer.TimerStoped += delegate
            {
                AssetDatabase.Refresh();
            };
            builder.DataChanged += delegate
            {
                refreshTimer.ResetTimer();
            };
        }

        protected virtual void OnDisable()
        {
            refreshTimer.StopCoroutine();
            AssetDatabase.Refresh();
        }
    }
}
