using UnityEngine;
using System.Collections;
namespace Kit.Performance
{
    public class PeDoUIShake : PeNode
    {
        public TransformTarget _targetGameObject;

        public float _offset;
        public float _duration;

        Vector2 _origanPosition;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            _origanPosition = _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().anchoredPosition;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();
            if (_elapsedTime > _duration)
            {
                PerformanceComplete();
            }else
            {
                float offset = _origanPosition.x + (Random.Range(0f, 1f) > 0.5f ? -_offset : _offset);
                _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, _origanPosition.y);
            }
        }

        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _targetGameObject.GetTarget(transform).GetComponent<RectTransform>().anchoredPosition = _origanPosition;
        }
    }
}