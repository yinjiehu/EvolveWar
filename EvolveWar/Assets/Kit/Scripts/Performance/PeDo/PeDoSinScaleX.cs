using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoSinScaleX : PeNode
    {
        public TransformTarget _targetGameObject;

        public float _duration;
        public float _radian;

        float _scaleX = 0;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _scaleX = 0;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();
            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
            else
            {
                _targetGameObject.GetTarget(transform).localScale = new Vector3(Mathf.Sin(_scaleX), 1, 1);
                _scaleX += _radian;
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
        }
    }
}
