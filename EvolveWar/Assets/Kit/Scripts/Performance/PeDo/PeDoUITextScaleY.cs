using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoUITextScaleY : PeNode
    {
        public RectTransform _targetGameObject;
        public AnimationCurve _globalSpeedCurve;
        //public float _scaleY;
        public float _duration;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _targetGameObject.localScale = Vector3.one;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();

            _targetGameObject.localScale = new Vector3(1, _globalSpeedCurve.Evaluate(_elapsedTime / _duration), 1);
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
