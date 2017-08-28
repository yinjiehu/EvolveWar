using UnityEngine;

namespace Kit.Performance
{
    public class PeDoUIChangeSizeDelta : PeNode
    {
        public TransformTarget _targetGameObject;

        public Vector2 from;
        public Vector2 to;
        public float _duration;

        Vector2 _originalSizeDelta;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _originalSizeDelta = _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().sizeDelta;
            _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().sizeDelta = from;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();

            _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(from, to, _elapsedTime / _duration);
            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().sizeDelta = _originalSizeDelta;
        }
    }
}
