using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoScaleStay : PeNode
    {
        public TransformTarget _transformTarget;
        public float _duration;
        public Vector3 _originScale = Vector3.one;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _transformTarget.GetTarget(transform).transform.localScale = _originScale;
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
