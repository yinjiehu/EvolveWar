using UnityEngine;

namespace Kit.Performance
{
    public class PeDoCanvasGroupFade : PeNode
    {
        public CanvasGroup _canvasGroup;
        public float _from;
        public float to;

        public float _duration;

        float _alpha;

        protected override void PerformanceStart()
        {
            base.PerformanceStart();
            _alpha = _canvasGroup.alpha;
            _canvasGroup.alpha = _from;
        }

        protected override void PerformancUpdate()
        {
            base.PerformancUpdate();

            if (_elapsedTime < _duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(_from, to, _elapsedTime / _duration);
            }
            else
            {
                PerformanceComplete();
            }
        }

        public override void PerformanceComplete()
        {
            base.PerformanceComplete();
            _canvasGroup.alpha = to;
        }
        public override void ResetToStartStatus()
        {
            base.ResetToStartStatus();
            _canvasGroup.alpha = _alpha;
        }

    }
}
