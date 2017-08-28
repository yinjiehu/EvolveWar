using UnityEngine;
using System.Collections;
using Kit.Performance;
namespace Kit.Performance
{
    public class PeDoUIMoveTo : PeNode
    {
        public RectTransform _targetUI;
        public AnimationCurve _globalSpeedCurve;
        public Vector2 from;
        public Vector2 to;
        public float _duration = 1;
        Vector3 _startPosition;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _elapsedTime = 0;
            _targetUI.anchoredPosition = from;
            _startPosition = _targetUI.localPosition;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();

            _targetUI.anchoredPosition = Vector2.Lerp(from, to, _globalSpeedCurve.Evaluate(_elapsedTime / _duration));

            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
        }

        public override void PerformanceComplete()
        {
            _targetUI.anchoredPosition = to;
            base.PerformanceComplete();
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _targetUI.localPosition = _startPosition;
        }
    }
}