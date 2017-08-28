using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kit.Performance
{
    public class PeDoSliderMove : PeNode
    {
        public Slider _slider;
        public float from;
        public float to;
        public float _duration = 1;
        float _lastRatio;
        public UnityEvent OnSliderFullCallback;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _elapsedTime = 0;
            _slider.value = from;
            _lastRatio = from;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();
            var time = _elapsedTime / _duration;

            float ratio = Mathf.Lerp(from, to, time);
            if (ratio >= Mathf.FloorToInt(_lastRatio + 1))
            {
                if (OnSliderFullCallback != null)
                {
                    OnSliderFullCallback.Invoke();
                }
            }
            _slider.value = ratio % 1f;
            _lastRatio = ratio;

            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
        }
    }
}
