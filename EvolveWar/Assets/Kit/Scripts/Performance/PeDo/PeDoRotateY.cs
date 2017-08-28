using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoRotateY : PeNode
    {
        public TransformTarget _targetGameObject;

        public float _duration;
        public float _speed;
        Quaternion _origanRatation;

        float _rotation = 0;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _origanRatation = _targetGameObject.GetTarget(transform).transform.localRotation;
            _rotation = 0;
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
                _targetGameObject.GetTarget(transform).localRotation = Quaternion.Euler(0, _rotation, 0);
                _rotation += _speed;
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _targetGameObject.GetTarget(transform).transform.localRotation = _origanRatation;
        }
    }
}
