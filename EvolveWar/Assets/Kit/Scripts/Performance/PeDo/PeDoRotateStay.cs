using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoRotateStay : PeNode
    {
        public TransformTarget _transformTarget;
        public float _duration;
        public Quaternion _originRotation;
		
        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _transformTarget.GetTarget(transform).transform.localRotation = _originRotation;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();
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
