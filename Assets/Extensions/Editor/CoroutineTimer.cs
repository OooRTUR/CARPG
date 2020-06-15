using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace Extensions.Editor
{
    public class CoroutineTimer
    {
        public event EventHandler TimerStoped;
        private EditorCoroutine coroutine;

        private bool timerUpdateRequired = false;
        private float initValue = 5.0f;
        private float updateTime;

        public CoroutineTimer(float initValue)
        {
            updateTime = initValue;
        }

        public void StartCoroutine()
        {
            coroutine = EditorCoroutineUtility.StartCoroutine(Coroutine(), this);
        }

        public void StopCoroutine()
        {
            EditorCoroutineUtility.StopCoroutine(coroutine);
        }

        private IEnumerator Coroutine()
        {
            while (true)
            {
                if (!timerUpdateRequired)
                {
                    yield return null;
                    continue;
                }
                if (updateTime <= 0.1f)
                {
                    StopTimer();
                }
                updateTime -= Time.deltaTime;
                yield return null;
            }
        }

        public void ResetTimer()
        {
            updateTime = initValue;
            timerUpdateRequired = true;
        }
        private void StopTimer()
        {
            timerUpdateRequired = false;
            TimerStoped?.Invoke(this, new EventArgs());

        }
    }
}
